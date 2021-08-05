using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BusinessLogic.Abstract
{
    public interface IMatchService
    {
        void UpdateDB(string Date);
        Match GetById(int Id);
    }
}
