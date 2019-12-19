using FreeGeoIPCore.AppCode;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocation_Emerging_Tech_Test.Models
{
    public static class WeatherGeolocation
    {
        public static string GetIPRequest(IHttpContextAccessor httpConAccessor, bool attemptUsingXForwardHeader = true)
        {
            string ipAddress = null;

            if(attemptUsingXForwardHeader)
            {
                ipAddress = GetHeaderValAs<string>(httpConAccessor, "X-Forwarded-For").SplitCsv().FirstOrDefault();
            }

            if (ipAddress.IsNullOrWhitespace() && httpConAccessor.HttpContext?.Connection?.RemoteIpAddress != null)
                ipAddress = httpConAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (ipAddress.IsNullOrWhitespace())
                ipAddress = GetHeaderValAs<string>(httpConAccessor, "REMOTE_ADDR");

            // _httpContextAccessor.HttpContext?.Request?.Host this is the local host.

            if (ipAddress.IsNullOrWhitespace())
                throw new Exception("Unable to determine caller's IP.");

            // Remove port if on IP address
            ipAddress = ipAddress.Substring(0,ipAddress.IndexOf(":"));


            return ipAddress;
        }


        public static T GetHeaderValAs<T>(IHttpContextAccessor httpConAccessor, string headerName)
        {
            StringValues vals;

            if (httpConAccessor.HttpContext?.Request?.Headers?.TryGetValue(headerName, out vals) ?? false)
            {
                string rawValues = vals.ToString();   // writes out as Csv when there are multiple.

                if (!rawValues.IsNullOrWhitespace())
                    return (T)Convert.ChangeType(vals.ToString(), typeof(T));
            }
            return default(T);
        }

    }
}
