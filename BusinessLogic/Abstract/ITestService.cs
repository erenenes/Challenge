using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Abstract
{
    public interface ITestService
    {
        List<Match> GetMatchs();
    }
}
