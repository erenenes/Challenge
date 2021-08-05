using MyProject.BusinessLogic.Abstract;
using MyProject.DataAccess.Abstract;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Concrete
{
    public class LogManager : ILogService
    {
        ILogDal _logDal;
        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }
        public void AddLog(string Status, string ErrorMessage, string Method, string ResponseJson)
        {
            _logDal.Add(new Log()
            {
                Status = Status,
                ErrorMessage = ErrorMessage,
                Method = Method,
                ResponseJson = ResponseJson,
                Date = DateTime.Now
            });
        }
    }
}
