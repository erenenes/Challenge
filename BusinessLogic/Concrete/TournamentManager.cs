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
    public class TournamentManager : ITournamentService
    {
        ILogService _logService;
        ITournamentDal _tournamentDal;
        IApiService _apiService;
        public TournamentManager(ILogService logService, ITournamentDal tournamentDal, IApiService apiService)
        {
            _logService = logService;
            _tournamentDal = tournamentDal;
            _apiService = apiService;
        }

        public Entities.Concrete.Tournament Add(Entities.Concrete.Tournament tournament)
        {
            var checkTournament = GetById(tournament.Id);
            if (checkTournament == null)
                _tournamentDal.Add(tournament);


            return tournament;
        }

        public Entities.Concrete.Tournament GetById(int Id)
        {
            return _tournamentDal.Get(x => x.Id == Id);
        }

        public Entities.Models.ResponseModel.Tournament GetTournamentById(int Id)
        {
            var tournamentResponseJson = _apiService.GetTournamentById(Id).Result;
            if (tournamentResponseJson != "Hata")
            {
                var jObjectTournament = JsonConvert.DeserializeObject<Entities.Models.ResponseModel.Tournament>(tournamentResponseJson);
                var tournament = GetById(jObjectTournament.id);
                if (tournament == null)
                    _tournamentDal.Add(new Entities.Concrete.Tournament()
                    {
                        Id = jObjectTournament.id,
                        Name = jObjectTournament.name,
                        ShortName = jObjectTournament.shortName
                    });
                return jObjectTournament;
            }
            else
                return null;

        }

    }
}
