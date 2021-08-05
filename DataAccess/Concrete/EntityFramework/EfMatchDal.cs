using MyProject.Core.DataAccess.EntityFramework;
using MyProject.DataAccess.Abstract;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.DataAccess.Concrete.EntityFramework
{
    public class EfMatchDal: EfEntityRepositoryBase<Match, MyProjectContext>,IMatchDal
    {
    }
}
