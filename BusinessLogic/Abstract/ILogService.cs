using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Abstract
{
    public interface ILogService
    {
        void AddLog(string Status, string ErrorMessage, string Method, string ResponseJson);
    }
}
