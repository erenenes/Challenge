using MyProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyProject.Entities.Concrete
{
    public class Match:IEntity
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int StatusId { get; set; }
        public int RoundId { get; set; }
        public int TournamentId { get; set; }
        public int StageId { get; set; }
        public int ScoreIdHome { get; set; }
        public int ScoreIdAway { get; set; }

        [ForeignKey("HomeTeamId")]
        public Team HomeTeam { get; set; }
        [ForeignKey("AwayTeamId")]
        public Team AwayTeam { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        [ForeignKey("RoundId")]
        public Round Round { get; set; }
        [ForeignKey("TournamentId")]
        public Tournament Tournament { get; set; }
        [ForeignKey("StageId")]
        public Stage Stage { get; set; }
        [ForeignKey("ScoreIdHome")]
        public Score HomeTeamScore { get; set; }
        [ForeignKey("ScoreIdAway")]
        public Score AwayTeamScore { get; set; }

    }
}
