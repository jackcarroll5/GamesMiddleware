using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeolocationGamesMiddlewareEmergingTech.Models;
using Microsoft.AspNetCore.Hosting;
using MaxMind.GeoIP2;
using Microsoft.Extensions.Logging;

namespace GeolocationGamesMiddlewareEmergingTech.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostEnvironment;

        public HomeController(IHostingEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index()
        {
            using (var readerData = new DatabaseReader(_hostEnvironment.ContentRootPath + "\\GeoLite2-City.mmdb"))
            {
                var ipAdd = HttpContext.Connection.RemoteIpAddress;

                var cityLocation = readerData.City(ipAdd);

                return View(cityLocation);
            }                     
        }    
    }
}
