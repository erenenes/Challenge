using MyProject.BusinessLogic.Abstract;
using MyProject.DataAccess.Abstract;
using MyProject.Entities.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static MyProject.Entities.Models.ResponseModel;

namespace MyProject.BusinessLogic.Concrete
{
    public class MatchManager : IMatchService
    {
        ILogService _logService;
        ITeamService _teamService;
        ITournamentService _tournamentService;
        IStageService _stageService;
        IScoreService _scoreService;
        IStatusService _statusService;
        IRoundService _roundService;

        IMatchDal _matchDal;
        IApiService _apiService;

        public MatchManager(IApiService apiService, IStatusService statusService, IRoundService roundService, ILogService logService, ITeamService teamService, ITournamentService tournamentService, IStageService stageService, IMatchDal matchDal, IScoreService scoreService)
        {
            _apiService = apiService;
            _logService = logService;
            _teamService = teamService;
            _tournamentService = tournamentService;
            _stageService = stageService;
            _matchDal = matchDal;
            _statusService = statusService;
            _roundService = roundService;
            _scoreService = scoreService;
        }

        public Match GetById(int Id)
        {
            return _matchDal.Get(x => x.Id == Id);

        }

        public void UpdateDB(string Date)
        {
            var statuListResponseJson = _apiService.GetAllStatus().Result;
            if (statuListResponseJson != "Hata")
            {
                var jObjectStatusList = JsonConvert.DeserializeObject<List<Entities.Models.ResponseModel.Status>>(statuListResponseJson);
                foreach (var item in jObjectStatusList)
                {
                    _statusService.Add(new MyProject.Entities.Concrete.Status()
                    {
                        Id = item.id,
                        Name = item.name,
                        ShortName = item.shortName
                    });
                }
            }
         
            var roundResponseJson = _apiService.GetAllRounds().Result;
            if (roundResponseJson != "Hata")
            {
                var jObjectRoundList = JsonConvert.DeserializeObject<List<Entities.Models.ResponseModel.Round>>(roundResponseJson);
                foreach (var item in jObjectRoundList)
                {
                    _roundService.Add(new MyProject.Entities.Concrete.Round()
                    {
                        Id = item.id,
                        Name = item.name,
                        ShortName = item.shortName
                    });
                }

            }
    
            var matchListResponseJson = _apiService.GetAllMatch(Date).Result;
            if (matchListResponseJson != "Hata")
            {
                var jObjectMatchList = JsonConvert.DeserializeObject<List<ResponseJsonModel>>(matchListResponseJson);
                foreach (var item in jObjectMatchList)
                {
                    var homeTeam = _teamService.GetById(item.homeTeam.id);
                    if (homeTeam == null)
                        _teamService.Add(new Team()
                        {
                            Id = item.homeTeam.id,
                            MediumName = item.homeTeam.mediumName,
                            Name = item.homeTeam.name,
                            ShortName = item.homeTeam.shortName
                        });
                    var awayTeam = _teamService.GetById(item.awayTeam.id);
                    if (awayTeam == null)
                        _teamService.Add(new Team()
                        {
                            Id = item.awayTeam.id,
                            MediumName = item.awayTeam.mediumName,
                            Name = item.awayTeam.name,
                            ShortName = item.awayTeam.shortName
                        });
                    var tournament = _tournamentService.GetTournamentById(item.tournament.id);
                  

                    var stage = _stageService.GetById(item.stage.id);
                    if (stage == null)
                        _stageService.Add(new Entities.Concrete.Stage()
                        {
                            Id = item.stage.id,
                            Name = item.stage.name,
                            ShortName = item.stage.shortName
                        });

                    var match = _matchDal.Get(x => x.Id == item.id);
                    if (match == null)
                    {
                        var homeScore = _scoreService.Add(new Entities.Concrete.Score()
                        {
                            MatchId = item.id,
                            TeamId = item.homeTeam.id,
                            Regular = item.homeTeam.score.regular,
                            Current = item.homeTeam.score.current,
                            HalfTime = item.homeTeam.score.halfTime
                        });
                        var awayScore = _scoreService.Add(new Entities.Concrete.Score()
                        {
                            MatchId = item.id,
                            TeamId = item.awayTeam.id,
                            Regular = item.awayTeam.score.regular,
                            Current = item.awayTeam.score.current,
                            HalfTime = item.awayTeam.score.halfTime
                        });
                        _matchDal.Add(new Match()
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
                }
            }

        }
    }
}
