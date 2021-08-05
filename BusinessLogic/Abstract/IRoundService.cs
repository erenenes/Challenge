using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Abstract
{
    public interface IRoundService
    {
        Round Add(Round status);
        Round GetById(int Id);
    }
}
