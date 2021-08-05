using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Abstract
{
  public interface IStatusService
    {
        Status Add(Status status);
        Status GetById(int Id);

    }
}
