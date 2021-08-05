using Microsoft.EntityFrameworkCore;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;


namespace MyProject.DataAccess.Concrete.EntityFramework
{
    public class MyProjectContext : DbContext
    {

        public DbSet<Match> Match { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<Tournament> Tournament { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<Log> Log { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-DEJ974U\\SQLEXPRESS;Initial Catalog=Calisma;Integrated Security=True");

        }
     
    }
}
