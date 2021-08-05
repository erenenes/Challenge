using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyProject.BusinessLogic.Abstract;
using MyProject.BusinessLogic.Concrete;
using MyProject.DataAccess.Abstract;
using MyProject.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHostedService<SchedulerService>();
            services.AddSingleton<ITestService, TestManager>();
            services.AddSingleton<ILogService, LogManager>();
            services.AddSingleton<IMatchService, MatchManager>();
            services.AddSingleton<ITeamService, TeamManager>();
            services.AddSingleton<IStageService, StageManager>();
            services.AddSingleton<IScoreService, ScoreManager>();
            services.AddSingleton<IStatusService, StatusManager>();
            services.AddSingleton<IRoundService, RoundManager>();
            services.AddSingleton<IApiService, ApiManager>();

            services.AddSingleton<ITournamentService, TournamentManager>();



            services.AddSingleton<IMatchDal, EfMatchDal>();
            services.AddSingleton<ILogDal, EfLogDal>();
            services.AddSingleton<IRoundDal, EfRoundDal>();
            services.AddSingleton<IScoreDal, EfScoreDal>();
            services.AddSingleton<IStageDal, EfStageDal>();
            services.AddSingleton<IStatusDal, EfStatusDal>();
            services.AddSingleton<ITeamDal, EfTeamDal>();
            services.AddSingleton<ITournamentDal, EfTournamentDal>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
