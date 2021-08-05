using MyProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProject.Entities.Concrete
{
    public class Log:IEntity
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseJson { get; set; }
        public DateTime Date { get; set; }
    }
}
