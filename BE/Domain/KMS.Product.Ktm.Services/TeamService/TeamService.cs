using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Dto.KmsTeam;
using KMS.Product.Ktm.Services.AuthenticateService;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private IConfiguration Configuration { get; }
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;
        private readonly ILogger<TeamService> _logger;

        /// <summary>
        /// Inject Team repository, AutoMapper, Authenticate service, Logger 
        /// </summary>
        /// <returns></returns>
        public TeamService(
            IConfiguration configuration,
            ITeamRepository teamRepository, 
            IMapper mapper, 
            IAuthenticateService authenticateService, 
            ILogger<TeamService> logger)
        {
            Configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)}");
            _teamRepository = teamRepository ?? throw new ArgumentNullException($"{nameof(teamRepository)}");
            _mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
            _authenticateService = authenticateService ?? throw new ArgumentNullException($"{nameof(authenticateService)}");
            _logger = logger ?? throw new ArgumentNullException($"{nameof(logger)}");
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

            if (team == null)
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
        public async Task SyncTeamDatabaseWithKmsAsync(DateTime now)
        {
            _logger.LogInformation("Start sync team");
            // Fetch teams from KMS and map from DTO to Team
            var fetchedTeamsDto = await GetTeamsFromKmsAsync();
            var fetchedDistinctTeamsDto = fetchedTeamsDto.GroupBy(t => t.TeamName)
                .Select(t => t.First())
                .ToList();
            var fetchedTeams = _mapper.Map<IEnumerable<KmsTeamDto>, IEnumerable<Team>>(fetchedDistinctTeamsDto);

            // Get teams from database
            var databaseTeams = await GetAllTeamsAsync();

            // Get team names
            var fetchedTeamNames = fetchedTeams.Select(e => e.TeamName).ToList();
            var databaseTeamNames = databaseTeams.Select(e => e.TeamName).ToList();

            // Sync new teams
            var newTeams = fetchedTeams.Where(e => !databaseTeamNames.Contains(e.TeamName)).ToList();
            if (newTeams.Count() > 0)
            {
                await SyncNewTeams(newTeams);
            }

            // Sync disband teams
            var disbandTeams = databaseTeams.Where(e => !fetchedTeamNames.Contains(e.TeamName)).ToList();
            if (disbandTeams.Count() > 0)
            {
                await SyncDisbandTeams(disbandTeams);
            }
        }

        /// <summary>
        /// Get all teams from KMS by KMS API
        /// API: https://hr.kms-technology.com/api/projects/ReturnListProjectClient
        /// </summary>
        /// <returns>A collection of all KMS team DTOs</returns>
        private async Task<IEnumerable<KmsTeamDto>> GetTeamsFromKmsAsync()
        {
            var kmsTeamDTOs = new List<KmsTeamDto>();

            // Initialize httpclient with token from login service to send request to KMS HRM 
            var bearerToken = await _authenticateService.AuthenticateUsingConfiguration();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var response = await client.GetAsync(Configuration.GetValue<string>("KmsInfo:KmsTeamRequestUrl"));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Convert response JSON to object
                var contentString = await response.Content.ReadAsStringAsync();
                var jsonKmsTeamDTOs = JsonConvert.DeserializeObject<IEnumerable<KmsTeamDto>>(contentString);

                // Add KMS team DTOs to list
                kmsTeamDTOs.AddRange(jsonKmsTeamDTOs);
            }

            return kmsTeamDTOs;
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
                newTeam.Active = true;
            }

            await _teamRepository.CreateTeams(newTeams);
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
                disband.Active = false;
            }

            await _teamRepository.UpdateTeams(disbandTeams);
        } 
    }
}
