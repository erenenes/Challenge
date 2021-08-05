using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Abstract
{
    public interface IScoreService
    {
        Score Add(Score score);
        Score GetByMatchIdAndTeamId(int MatchId,int TeamId);

    }
}
