using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.LoginService;
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
        private readonly ILoginService _loginService;

        /// <summary>
        /// Inject Team repository
        /// </summary>
        /// <returns></returns>
        public TeamService(ITeamRepository teamRepository, IMapper mapper, ILoginService loginService)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException($"{nameof(teamRepository)}");
            _mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
            _loginService = loginService ?? throw new ArgumentNullException($"{nameof(loginService)}");
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
        /// <returns>An team by id</returns>
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await _teamRepository.GetByIdAsync(teamId);
        }

        /// <summary>
        /// Create team
        /// </summary>
        /// <returns></returns>
        public async Task CreateTeamAsync(Team team)
        {
            await _teamRepository.InsertAsync(team);
        }

        /// <summary>
        /// Update team
        /// </summary>
        /// <returns></returns>
        public async Task UpdateTeamAsync(Team team)
        {
            await _teamRepository.UpdateAsync(team);
        }

        /// <summary>
        /// Delete team
        /// </summary>
        /// <returns></returns>
        public async Task DeleteTeamAsync(Team team)
        {
            await _teamRepository.DeleteAsync(team);
        }

        /// <summary>
        /// Get team Id from team name
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns>
        /// If team exists, return team id
        /// Else, create new team and return new team id
        /// </returns>
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
        /// Get all teams from KMS HRM system
        /// https://hr.kms-technology.com/api/projects/ReturnListProjectClient
        /// </summary>
        /// <returns>A collection of all teams from KMS HRM system</returns>
        public async Task<IEnumerable<TeamFromKmsDto>> GetTeamsFromKmsAsync()
        {
            var teamsFromKms = new List<TeamFromKmsDto>();
            // Initialize httpclient with token from login service to send request to KMS HRM 
            //var bearerToken = await _loginService.LoginWithConfiguration();
            var bearerToken = _loginService.LoginWithTokens();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var requestUrl = "https://hr.kms-technology.com/api/projects/ReturnListProjectClient";
            var response = await client.GetAsync(requestUrl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Convert response JSON to object
                var contentString = await response.Content.ReadAsStringAsync();
                var kmsTeamResponse = JsonConvert.DeserializeObject<TeamFromKmsResponse>(contentString);
                // Add KMS teams to list
                var kmsTeamDtos = kmsTeamResponse.KmsTeamDtos;
                teamsFromKms.AddRange(kmsTeamDtos);
            }
            return teamsFromKms;
        }

        /// <summary>
        /// Add new teams from KMS 
        /// </summary>
        /// <param name="newTeams"></param>
        /// <returns></returns>
        public async Task SyncNewTeams(IEnumerable<Team> newTeams)
        {
            foreach (var newTeam in newTeams)
            {
                await CreateTeamAsync(newTeam);
            }
        }

        /// <summary>
        /// Update teams in database from KMS
        /// </summary>
        /// <param name="currentTeams"></param>
        /// <returns></returns>
        public async Task SyncCurrentTeams(IEnumerable<Team> currentTeams)
        {
            foreach (var currentTeam in currentTeams)
            {
                await UpdateTeamAsync(currentTeam);
            }
        }

        /// <summary>
        /// Update quit employee in database by setting released date to current
        /// </summary>
        /// <param name="quitTeams"></param>
        /// <returns></returns>
        public async Task SyncQuitTeams(IEnumerable<Team> quitTeams)
        {
            foreach (var oldTeam in quitTeams)
            {
                await UpdateTeamAsync(oldTeam);
            }
        }

        /// <summary>
        /// Add new teams to database from list of all teams after GET request to KMS HRM API
        /// If team's badge is not in database, it is a new team 
        /// </summary>
        /// <returns></returns>
        public async Task SyncTeamDatabaseWithKmsAsync()
        {
            // Fetch employees from KMS and map from DTO to Employee
            var fetchedTeamsDto = await GetTeamsFromKmsAsync();
            var fetchedTeams = _mapper.Map<IEnumerable<TeamFromKmsDto>, IEnumerable<Team>>(fetchedTeamsDto);
            // Get teams from database
            var databaseTeams = await GetAllTeamsAsync();
            // Get team names
            var fetchedTeamNames = fetchedTeams.Select(e => e.TeamName).ToList();
            var databaseTeamNames = databaseTeams.Select(e => e.TeamName).ToList();
            // Sync new teams
            var newTeams = fetchedTeams.Where(e => !databaseTeamNames.Contains(e.TeamName));
            await SyncNewTeams(newTeams);
            // Sync current teams
            var currentTeams = fetchedTeams.Where(e => databaseTeamNames.Contains(e.TeamName));
            await SyncCurrentTeams(currentTeams);
            // Sync quit teams
            var quitTeams = databaseTeams.Where(e => !fetchedTeamNames.Contains(e.TeamName));
            await SyncQuitTeams(quitTeams);
        }
    }
}
