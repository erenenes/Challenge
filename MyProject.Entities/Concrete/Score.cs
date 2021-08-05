using MyProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProject.Entities.Concrete
{
    public class Score:IEntity
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public int Regular { get; set; }
        public int HalfTime { get; set; }
        public int Current { get; set; }
    }
}
