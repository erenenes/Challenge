using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Abstract
{
    public interface ITeamService
    {
        Team GetById(int Id);
        Team Add(Team team);
    }
}
