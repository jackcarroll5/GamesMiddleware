using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Geolocation_Emerging_Tech_Test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using FreeGeoIPCore;
using MetOfficeDataPoint;
using MetOfficeDataPoint.Models;
using MetOfficeDataPoint.Models.GeoCoordinate;


namespace Geolocation_Emerging_Tech_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpConAccessor;
        private readonly IConfiguration _con;



        public HomeController(IConfiguration conf, IHttpContextAccessor httpConAccessor)
        {
            _con = conf;
            _httpConAccessor = httpConAccessor;
        }


        public IActionResult Index(double lon, double lat)
        {
            FreeGeoIPClient clientIP = new FreeGeoIPClient();      

             string ipAdd = WeatherGeolocation.GetIPRequest(_httpConAccessor);

            FreeGeoIPCore.Models.Location location = clientIP.GetLocation(ipAdd).Result;

            GeoCoordinate geoCoordinate = new GeoCoordinate();

            if(lon == 0 && lat == 0)
            {
                geoCoordinate.Longitude = location.Longitude;
                geoCoordinate.Latitude = location.Latitude;
            }
            else
            {
                geoCoordinate.Longitude = lon;
                geoCoordinate.Latitude = lat;
            }
            return View();
        }     
    }
}
