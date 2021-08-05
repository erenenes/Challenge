using MyProject.BusinessLogic.Abstract;
using MyProject.DataAccess.Abstract;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Concrete
{
    public class TestManager : ITestService
    {
        IMatchDal _matchDal;

        public TestManager(IMatchDal matchDal)
        {
            _matchDal = matchDal;
        }

        public List<Match> GetMatchs()
        {
            var matchList = _matchDal.GetList(null, new string[] { "HomeTeam", "AwayTeam", "Status", "Round", "Tournament", "Stage", "HomeTeamScore", "AwayTeamScore" });
            return matchList;
        }
    }
}
