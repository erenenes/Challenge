using MyProject.BusinessLogic.Abstract;
using MyProject.DataAccess.Abstract;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Concrete
{
    public class TeamManager : ITeamService
    {
        ITeamDal _teamDal;
        public TeamManager(ITeamDal teamDal)
        {
            _teamDal = teamDal;
        }

        public Team Add(Team team)
        {
            var checkTeam = GetById(team.Id);
            if (checkTeam == null)
                _teamDal.Add(team);
           
            return team;
        }

        public Team GetById(int Id)
        {
            return _teamDal.Get(x => x.Id == Id);
        }
    }
}
