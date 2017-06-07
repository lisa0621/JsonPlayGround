using JsonPlayGround.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JsonPlayGround.Controllers
{
    public class ShulinParkingLotController : Controller
    {
        // GET: ShulinParkingLot
        public async Task<ActionResult> Index()
        {
            string targetURI = "http://data.ntpc.gov.tw/api/v1/rest/datastore/382000000A-G00218-002";

            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetURI);

            JObject resultJO = JObject.Parse(response);

            // get JSON result objects into a list
            string results = resultJO["result"]["records"].ToString();

            List<ShulinParkingLotVM> collection = JsonConvert.DeserializeObject<List<ShulinParkingLotVM>>(results);
            
            return View(collection);
        }
    }
}