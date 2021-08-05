using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BusinessLogic.Abstract
{
    public interface ITournamentService
    {
        Entities.Models.ResponseModel.Tournament GetTournamentById(int Id);
        Tournament Add(Tournament tournament);
        Tournament GetById(int Id);
    }
}
