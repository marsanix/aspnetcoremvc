using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using ASPNet_Core_MVC.Models;

namespace ASPNet_Core_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient client = new HttpClient();
        private static String apiurl = "http://api.arrow.com/itemservice/v3/en/search/list";
        private static String apilogin = "supremecomponents";
        private static String apikey = "07b23129ead7328ca4f14a9c08fa89f333e30d08042a5ec4d211e7b66851825d";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var clientip = "127.0.0.1";
            var parts = "[{\"partNum\":\"24AA256-I/MS\",\"mfr\":\"MICRO CHIP\"},{\"partNum\":\"LT1805CS%23PBF\",\"mfr\":\"Arrow\'s\"},{\"partNum\":\"MAX32,32CAE\",\"mfr\":\"MAXIM\"},{\"partNum\":\"MIC5319-3.3YD5-.TR\"},{\"partNum\":\"SSL1523P/N2112\",\"mfr\":\"NXP\"}]";
            var url = apiurl + "?req={\"request\":{\"login\":\"" + apilogin + "\",\"apikey\":\"" + apikey + "\",\"remoteIp\":\"" + clientip + "\",\"useExact\":true,\"parts\":" + parts + "}}";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                dynamic jsonObj2 = JsonConvert.DeserializeObject(result);
                var data = jsonObj2.itemserviceresult.data[0];
                var resultList = data.resultList;
       
                List<Parts> PartList = new List<Parts>();         
                foreach (var prod in resultList)
                {
      
                    if (prod.PartList.Count > 0)
                    {
                        PartList.Add(new Parts(
                            string.Format("{0}", prod.PartList[0].itemId),
                            string.Format("{0}", prod.requestedPartNum),
                            string.Format("{0}", prod.PartList[0].manufacturer.mfrName),
                            string.Format("{0}", prod.PartList[0].desc)
                            ));
                    }
                    else
                    {
                        PartList.Add(new Parts(
                            "-",
                            string.Format("{0}", prod.requestedPartNum),
                            "-",
                            "-"
                            ));
                    }      
                }    

                ViewData["PartList"] = PartList;

            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
