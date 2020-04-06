using AutoMapper;
using KMS.Product.Ktm.Dto;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.AuthenticateService;
using KMS.Product.Ktm.Services.RepoInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;

        public const string KmsTeamRequestUrl = "https://hr.kms-technology.com/api/projects/ReturnListProjectClient";

        /// <summary>
        /// Inject Team repository, AutoMapper, Authenticate service
        /// </summary>
        /// <returns></returns>
        public TeamService(ITeamRepository teamRepository, IMapper mapper, IAuthenticateService authenticateService)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException($"{nameof(teamRepository)}");
            _mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
            _authenticateService = authenticateService ?? throw new ArgumentNullException($"{nameof(authenticateService)}");
        }

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns>A collection of all teams</returns>
        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetTeamsAsync();
        }

        /// <summary>
        /// Get team by id
        /// </summary>
        /// <returns>Team by id</returns>
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await _teamRepository.GetByIdAsync(teamId);
        }
        /// <summary>
        /// Create a new team
        /// </summary>
        /// <returns></returns>
        public async Task CreateTeamAsync(Team team)
        {
            await _teamRepository.InsertAsync(team);
        }

        /// <summary>
        /// Update an existent team
        /// </summary>
        /// <returns></returns>
        public async Task UpdateTeamAsync(Team team)
        {
            await _teamRepository.UpdateAsync(team);
        }

        /// <summary>
        /// Delete an existent team
        /// </summary>
        /// <returns></returns>
        public async Task DeleteTeamAsync(Team team)
        {
            await _teamRepository.DeleteAsync(team);
        }

        /// <summary>
        /// Get team id from team name
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns>
        /// If team exists, return team id
        /// Else, create a new team and return that new team id
        /// </returns>s
        public async Task<int> GetTeamIdByTeamNameAsync(string teamName)
        {
            var team = (await _teamRepository.GetTeamsByConditionAsync(t => t.TeamName == teamName)).SingleOrDefault();

            if (team.Equals(null))
            {
                var newTeam = new Team()
                {
                    TeamName = teamName
                };
                await CreateTeamAsync(newTeam);
                return newTeam.Id;
            }

            return team.Id;
        }

        /// <summary>
        /// There are 3 cases when syncing:
        /// 1. New teams
        ///     Add new teams to database
        /// 2. Active teams
        /// 3. Disband teams
        /// </summary>
        /// <returns></returns>
        public async Task SyncTeamDatabaseWithKmsAsync()
        {
            // Fetch teams from KMS and map from DTO to Team
            var fetchedTeamsDto = await GetTeamsFromKmsAsync();
            var fetchedTeams = _mapper.Map<IEnumerable<KmsTeamDto>, IEnumerable<Team>>(fetchedTeamsDto);
            // Get teams from database
            var databaseTeams = await GetAllTeamsAsync();
            // Get team names
            var fetchedTeamNames = fetchedTeams.Select(e => e.TeamName).ToList();
            var databaseTeamNames = databaseTeams.Select(e => e.TeamName).ToList();
            // Sync new teams
            var newTeams = fetchedTeams.Where(e => !databaseTeamNames.Contains(e.TeamName));
            await SyncNewTeams(newTeams);
            // Sync active teams
            var activeTeams = fetchedTeams.Where(e => databaseTeamNames.Contains(e.TeamName));
            await SyncActiveTeams(activeTeams);
            // Sync disband teams
            var disbandTeams = databaseTeams.Where(e => !fetchedTeamNames.Contains(e.TeamName));
            await SyncDisbandTeams(disbandTeams);
        }

        /// <summary>
        /// Get all teams from KMS by KMS API
        /// API: https://hr.kms-technology.com/api/projects/ReturnListProjectClient
        /// </summary>
        /// <returns>A collection of all KMS team DTOs</returns>
        private async Task<IEnumerable<KmsTeamDto>> GetTeamsFromKmsAsync()
        {
            var KmsTeamDTOs = new List<KmsTeamDto>();
            // Initialize httpclient with token from login service to send request to KMS HRM 
            var bearerToken = _authenticateService.AuthenticateUsingToken();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var response = await client.GetAsync(KmsTeamRequestUrl);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Convert response JSON to object
                var contentString = await response.Content.ReadAsStringAsync();
                var kmsTeamResponse = JsonConvert.DeserializeObject<KmsTeamResponseDto>(contentString);
                // Add KMS team DTOs to list
                KmsTeamDTOs.AddRange(kmsTeamResponse.KmsTeams);
            }

            return KmsTeamDTOs;
        }

        /// <summary>
        /// Add new teams from KMS 
        /// </summary>
        /// <param name="newTeams"></param>
        /// <returns></returns>
        private async Task SyncNewTeams(IEnumerable<Team> newTeams)
        {
            foreach (var newTeam in newTeams)
            {
                await CreateTeamAsync(newTeam);
            }
        }

        /// <summary>
        /// Update active teams in database 
        /// </summary>
        /// <param name="activeTeams"></param>
        /// <returns></returns>
        private async Task SyncActiveTeams(IEnumerable<Team> activeTeams)
        {
            foreach (var activeTeam in activeTeams)
            {
                await UpdateTeamAsync(activeTeam);
            }
        }

        /// <summary>
        /// Update disband teams in database 
        /// </summary>
        /// <param name="disbandTeams"></param>
        /// <returns></returns>
        private async Task SyncDisbandTeams(IEnumerable<Team> disbandTeams)
        {
            foreach (var disband in disbandTeams)
            {
                await UpdateTeamAsync(disband);
            }
        }
    }
}
