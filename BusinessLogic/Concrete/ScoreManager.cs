using MyProject.BusinessLogic.Abstract;
using MyProject.DataAccess.Abstract;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Concrete
{
    public class ScoreManager : IScoreService
    {
        IScoreDal _scoreDal;
        public ScoreManager(IScoreDal scoreDal)
        {
            _scoreDal = scoreDal;
        }
        public Score Add(Score score)
        {
            var checkScore = GetByMatchIdAndTeamId(score.MatchId,score.TeamId);
            if (checkScore == null)
                _scoreDal.Add(score);

            return score;
        }

        public Score GetByMatchIdAndTeamId(int MatchId, int TeamId)
        {
            return _scoreDal.Get(x => x.MatchId == MatchId && x.TeamId==TeamId);

        }
    }
}
