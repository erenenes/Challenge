
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using MyProject.DataAccess.Concrete.EntityFramework;
using MyProject.Entities.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static ConsoleApp1.Models.ResponseJsonModel;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {

            while (true)
            {
                RunAsync().Wait();
                Thread.Sleep(20000);
            }
            //RunAsync().Wait();
        }
        

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Task başlıyor");
                client.BaseAddress = new Uri("https://s0-sports-data-api.broadage.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "64c92943-67bd-47bc-bf51-56ad5ea04db5");
                client.DefaultRequestHeaders.Add("languageId", "2");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                #region Match List Request
                HttpResponseMessage matchListResponse = await client.GetAsync("soccer/match/list?date=02/08/2021");
                string matchListResponseJson = await matchListResponse.Content.ReadAsStringAsync();
                if (matchListResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorModel = !string.IsNullOrEmpty(matchListResponseJson)?JsonConvert.DeserializeObject<ErrorMessage>(matchListResponseJson):new ErrorMessage() {statusCode= matchListResponse.StatusCode.ToString(),message= matchListResponse.StatusCode.ToString() };
                    using (var context = new MyProjectContext())
                    {
                        context.Log.Add(new Log()
                        {
                            Status = errorModel.statusCode,
                            ErrorMessage = errorModel.message,
                            Method = "soccer/match/list?date=02/08/2021",
                            ResponseJson = matchListResponseJson,
                            Date = DateTime.Now
                        });
                        context.SaveChanges();
                    }
                }
                else
                {
                    var jObjectMatchList = JsonConvert.DeserializeObject<List<Root>>(matchListResponseJson);
                    using (var context = new MyProjectContext())
                    {
                        foreach (var item in jObjectMatchList)
                        {
                            var homeTeam = context.Team.Where(x => x.Id == item.homeTeam.id).ToList();
                            var awayTeam = context.Team.Where(x => x.Id == item.awayTeam.id).ToList();
                            if (homeTeam.Count() == 0)
                            {
                                context.Team.Add(new Team()
                                {
                                    Id = item.homeTeam.id,
                                    MediumName = item.homeTeam.mediumName,
                                    Name = item.homeTeam.name,
                                    ShortName = item.homeTeam.shortName
                                });
                            }
                            if (awayTeam.Count() == 0)
                            {
                                context.Team.Add(new Team()
                                {
                                    Id = item.awayTeam.id,
                                    MediumName = item.awayTeam.mediumName,
                                    Name = item.awayTeam.name,
                                    ShortName = item.awayTeam.shortName
                                });
                            }

                            //Tournement Request
                            var tournementId = item.tournament.id;
                            HttpResponseMessage tournementResponse = await client.GetAsync("/soccer/tournament/info?tournamentId=" + tournementId);
                            string tournementResponseJson = await tournementResponse.Content.ReadAsStringAsync();
                            var jObjectTournement = JsonConvert.DeserializeObject<ResponseJsonModel.Tournament>(tournementResponseJson);
                            if (tournementResponse.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                var errorModel = !string.IsNullOrEmpty(tournementResponseJson) ? JsonConvert.DeserializeObject<ErrorMessage>(matchListResponseJson) : new ErrorMessage() { statusCode = tournementResponse.StatusCode.ToString(), message = tournementResponse.StatusCode.ToString() };
                                context.Log.Add(new Log()
                                {
                                    Status = errorModel.statusCode,
                                    ErrorMessage = errorModel.message,
                                    Method = "/soccer/tournament/info?tournamentId=" + tournementId,
                                    ResponseJson = tournementResponseJson,
                                    Date = DateTime.Now

                                });
                            }
                            else
                            {
                                if (context.Tournament.Where(x => x.Id == jObjectTournement.id).ToList().Count() == 0)
                                {
                                    context.Tournament.Add(new MyProject.Entities.Concrete.Tournament()
                                    {
                                        Id = jObjectTournement.id,
                                        Name = jObjectTournement.name,
                                        ShortName = jObjectTournement.shortName
                                    });
                                }
                                context.Log.Add(new Log()
                                {
                                    Status = "200",
                                    Method = "/soccer/tournament/info?tournamentId=" + tournementId,
                                    ResponseJson = tournementResponseJson,
                                    Date = DateTime.Now
                                });
                            }

                            if (context.Stage.Where(x => x.Id == item.stage.id).ToList().Count() == 0)
                            {
                                context.Stage.Add(new MyProject.Entities.Concrete.Stage()
                                {
                                    Id = item.stage.id,
                                    Name = item.stage.name,
                                    ShortName = item.stage.shortName
                                });
                            }

                            if (context.Match.Where(x => x.Id == item.id).ToList().Count() == 0)
                            {
                                // homeTeam Score
                                MyProject.Entities.Concrete.Score homeScore = new MyProject.Entities.Concrete.Score()
                                {
                                    MatchId = item.id,
                                    TeamId = item.homeTeam.id,
                                    Regular = item.homeTeam.score.regular,
                                    Current = item.homeTeam.score.current,
                                    HalfTime = item.homeTeam.score.halfTime
                                };
                                context.Score.Add(homeScore);
                                // awayTeam Score
                                MyProject.Entities.Concrete.Score awayScore = new MyProject.Entities.Concrete.Score()
                                {
                                    MatchId = item.id,
                                    TeamId = item.awayTeam.id,
                                    Regular = item.awayTeam.score.regular,
                                    Current = item.awayTeam.score.current,
                                    HalfTime = item.awayTeam.score.halfTime
                                };
                                context.Score.Add(awayScore);

                                context.SaveChanges();

                                //Match Table 
                                context.Match.Add(new Match()
                                {
                                    Id = item.id,
                                    Date = item.date,
                                    HomeTeamId = item.homeTeam.id,
                                    AwayTeamId = item.awayTeam.id,
                                    StatusId = item.status.id,
                                    RoundId = item.round.id,
                                    TournamentId = item.tournament.id,
                                    StageId = item.stage.id,
                                    ScoreIdHome = homeScore.Id,
                                    ScoreIdAway = awayScore.Id
                                });
                            }
                            context.SaveChanges();
                        }
                        context.Log.Add(new Log()
                        {
                            Status = "200",
                            Method = "soccer/match/list?date=02/08/2021",
                            ResponseJson = matchListResponseJson,
                            Date = DateTime.Now
                        });
                        context.SaveChanges();

                    }

                }
                #endregion

                #region Status List Request
                HttpResponseMessage statuListResponse = await client.GetAsync("/global/status/list");
                string statuListResponseJson = await statuListResponse.Content.ReadAsStringAsync();
                if (statuListResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorModel = !string.IsNullOrEmpty(statuListResponseJson) ? JsonConvert.DeserializeObject<ErrorMessage>(statuListResponseJson) : new ErrorMessage() { statusCode = statuListResponse.StatusCode.ToString(), message = statuListResponse.StatusCode.ToString() };
                    using (var context = new MyProjectContext())
                    {
                        context.Log.Add(new Log()
                        {
                            Status = errorModel.statusCode,
                            ErrorMessage = errorModel.message,
                            Method = "/global/status/list",
                            ResponseJson = statuListResponseJson,
                            Date = DateTime.Now

                        });
                        context.SaveChanges();
                    }
                }
                else
                {
                    var jObjectStatusList = JsonConvert.DeserializeObject<List<ResponseJsonModel.Status>>(statuListResponseJson);
                    using (var context = new MyProjectContext())
                    {
                        foreach (var item in jObjectStatusList)
                        {
                            if (context.Status.Where(x => x.Id == item.id).ToList().Count() == 0)
                            {
                                context.Status.Add(new MyProject.Entities.Concrete.Status()
                                {
                                    Id = item.id,
                                    Name = item.name,
                                    ShortName = item.shortName
                                });
                            }
                        }
                        context.Log.Add(new Log()
                        {
                            Status = "200",
                            Method = "/global/status/list",
                            ResponseJson = statuListResponseJson,
                            Date = DateTime.Now
                        });
                        context.SaveChanges();
                    }
                }


                #endregion

                #region Round List Request
                HttpResponseMessage roundListResponse = await client.GetAsync("/global/round/list");
                string roundListResponseJson = await roundListResponse.Content.ReadAsStringAsync();
                if (roundListResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorModel = !string.IsNullOrEmpty(roundListResponseJson) ? JsonConvert.DeserializeObject<ErrorMessage>(roundListResponseJson) : new ErrorMessage() { statusCode = roundListResponse.StatusCode.ToString(), message = roundListResponse.StatusCode.ToString() };

                    using (var context = new MyProjectContext())
                    {
                        context.Log.Add(new Log()
                        {
                            Status = errorModel.statusCode,
                            ErrorMessage = errorModel.message,
                            Method = "/global/round/list",
                            ResponseJson = roundListResponseJson,
                            Date = DateTime.Now
                        });
                        context.SaveChanges();
                    }
                }
                else
                {
                    var jObjectRoundList = JsonConvert.DeserializeObject<List<ResponseJsonModel.Status>>(roundListResponseJson);
                    using (var context = new MyProjectContext())
                    {
                        foreach (var item in jObjectRoundList)
                        {
                            if (context.Round.Where(x => x.Id == item.id).ToList().Count() == 0)
                            {
                                context.Round.Add(new MyProject.Entities.Concrete.Round()
                                {
                                    Id = item.id,
                                    Name = item.name,
                                    ShortName = item.shortName
                                });
                            }
                        }
                        context.Log.Add(new Log()
                        {
                            Status = "200",
                            Method = "/global/round/list",
                            ResponseJson = roundListResponseJson,
                            Date = DateTime.Now
                        });
                        context.SaveChanges();
                    }
                }



                #endregion
                Console.WriteLine("Task bitti");
            }
        }

    }
}
