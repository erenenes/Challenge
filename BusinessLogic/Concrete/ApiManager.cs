using MyProject.BusinessLogic.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static MyProject.Entities.Models.ResponseModel;

namespace MyProject.BusinessLogic.Concrete
{
    public class ApiManager : IApiService
    {
        ILogService _logService;
        public ApiManager(ILogService logService)
        {
            _logService = logService;
        }
        public async Task<string> GetAllMatch(string Date)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://s0-sports-data-api.broadage.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "64c92943-67bd-47bc-bf51-56ad5ea04db5");
                client.DefaultRequestHeaders.Add("languageId", "2");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage matchListResponse = await client.GetAsync("soccer/match/list?date=" + Date);
                string matchListResponseJson = await matchListResponse.Content.ReadAsStringAsync();
                if (matchListResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorModel = !string.IsNullOrEmpty(matchListResponseJson) ? JsonConvert.DeserializeObject<ErrorMessage>(matchListResponseJson) : new ErrorMessage() { statusCode = matchListResponse.StatusCode.ToString(), message = matchListResponse.StatusCode.ToString() };
                    _logService.AddLog(errorModel.statusCode, errorModel.message, "soccer/match/list?date=" + Date, matchListResponseJson);
                    return "Hata";
                }
                else
                {
                    _logService.AddLog("200", matchListResponse.StatusCode.ToString(), "soccer/match/list?date=" + Date, matchListResponseJson);
                    return matchListResponseJson;
                }
            }
        }

        public async Task<string> GetAllStatus()
        {
            #region Status List Request
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://s0-sports-data-api.broadage.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "64c92943-67bd-47bc-bf51-56ad5ea04db5");
                client.DefaultRequestHeaders.Add("languageId", "2");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage statuListResponse = await client.GetAsync("/global/status/list");
                string statuListResponseJson = await statuListResponse.Content.ReadAsStringAsync();
                if (statuListResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorModel = !string.IsNullOrEmpty(statuListResponseJson) ? JsonConvert.DeserializeObject<ErrorMessage>(statuListResponseJson) : new ErrorMessage() { statusCode = statuListResponse.StatusCode.ToString(), message = statuListResponse.StatusCode.ToString() };
                    _logService.AddLog(errorModel.statusCode, errorModel.message, "/global/status/list", statuListResponseJson);
                    return "Hata";
                }
                else
                {
                    _logService.AddLog("200", statuListResponse.StatusCode.ToString(), "/global/status/list", statuListResponseJson);
                    return statuListResponseJson;

                }
            }
            #endregion
        }

        public async Task<string> GetAllRounds()
        {
            #region Round List Request
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://s0-sports-data-api.broadage.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "64c92943-67bd-47bc-bf51-56ad5ea04db5");
                client.DefaultRequestHeaders.Add("languageId", "2");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage roundListResponse = await client.GetAsync("/global/round/list");
                string roundListResponseJson = await roundListResponse.Content.ReadAsStringAsync();
                if (roundListResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorModel = !string.IsNullOrEmpty(roundListResponseJson) ? JsonConvert.DeserializeObject<ErrorMessage>(roundListResponseJson) : new ErrorMessage() { statusCode = roundListResponse.StatusCode.ToString(), message = roundListResponse.StatusCode.ToString() };
                    _logService.AddLog(errorModel.statusCode, errorModel.message, "/global/round/list", roundListResponseJson);
                    return "Hata";

                }
                else
                {
                    _logService.AddLog("200", roundListResponse.StatusCode.ToString(), "/global/round/list", roundListResponseJson);
                    return roundListResponseJson;
                }
            }
            #endregion

        }

        public async Task<string> GetTournamentById(int TournamentId)
        {
            #region Tournament
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://s0-sports-data-api.broadage.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "64c92943-67bd-47bc-bf51-56ad5ea04db5");
                client.DefaultRequestHeaders.Add("languageId", "2");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage tournamentResponse = await client.GetAsync("/soccer/tournament/info?tournamentId=" + TournamentId);
                string tournamentResponseJson = await tournamentResponse.Content.ReadAsStringAsync();
                if (tournamentResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorModel = !string.IsNullOrEmpty(tournamentResponseJson) ? JsonConvert.DeserializeObject<ErrorMessage>(tournamentResponseJson) : new ErrorMessage() { statusCode = tournamentResponse.StatusCode.ToString(), message = tournamentResponse.StatusCode.ToString() };
                    _logService.AddLog(errorModel.statusCode, errorModel.message, "/soccer/tournament/info?tournamentId=" + TournamentId, tournamentResponseJson);
                    return "Hata";

                }
                else
                {
                    _logService.AddLog("200", tournamentResponse.StatusCode.ToString(), "/soccer/tournament/info?tournamentId=" + TournamentId, tournamentResponseJson);
                    return tournamentResponseJson;
                    
                }
            }
            #endregion

        }
    }
}
