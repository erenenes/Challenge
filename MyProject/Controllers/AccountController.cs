using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.API.Builder;
using InstaSharper.Logger;
using InstaSharper.Classes.Models;
using MyProject.BusinessLogic.Abstract;

namespace MyProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        ITestService _testService;
        

        public AccountController(ILogger<AccountController> logger,ITestService testService)
        {
            _logger = logger;
            _testService = testService;
            
        }

        public IActionResult Index()
        {

            var model = _testService.GetMatchs();
            return View();
        }

    

    }
}
