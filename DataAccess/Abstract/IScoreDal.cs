using MyProject.Core.DataAccess;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.DataAccess.Abstract
{
    public interface IScoreDal : IEntityRepository<Score>
    {
    }
}
