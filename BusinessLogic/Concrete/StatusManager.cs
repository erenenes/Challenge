using MyProject.BusinessLogic.Abstract;
using MyProject.DataAccess.Abstract;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Concrete
{
    public class StatusManager : IStatusService
    {
        IStatusDal _statusDal;
        public StatusManager(IStatusDal statusDal)
        {
            _statusDal = statusDal;
        }

        public Status Add(Status status)
        {
            var checkStatus = GetById(status.Id);
            if (checkStatus == null)
                _statusDal.Add(status);

            return status;
        }

        public Status GetById(int Id)
        {
            return _statusDal.Get(x => x.Id == Id);

        }
    }
}
