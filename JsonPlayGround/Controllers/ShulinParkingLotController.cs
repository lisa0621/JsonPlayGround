using JsonPlayGround.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
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
            var lotSource = await this.GetShulinParkingLotData();
            ViewData.Model = lotSource;
            return View();
        }

        private async Task<IEnumerable<ShulinParkingLotVM>> GetShulinParkingLotData()
        {
            string cacheName = "Shulin_ParkingLot";

            ObjectCache cache = MemoryCache.Default;
            CacheItem cacheContents = cache.GetCacheItem(cacheName);

            if (cacheContents == null)
            {
                return await RetriveShulinParkingLotData(cacheName);
            }
            else
            {
                return cacheContents.Value as IEnumerable<ShulinParkingLotVM>;
            }
        }

        private async Task<IEnumerable<ShulinParkingLotVM>> RetriveShulinParkingLotData(string cacheName)
        {
            string targetURI = "http://data.ntpc.gov.tw/api/v1/rest/datastore/382000000A-G00218-002";

            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetURI);

            JObject resultJO = JObject.Parse(response);

            // get JSON result objects into a list
            string results = resultJO["result"]["records"].ToString();

            IEnumerable<ShulinParkingLotVM> collection = JsonConvert.DeserializeObject<IEnumerable<ShulinParkingLotVM>>(results);

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(30);

            ObjectCache cacheItem = MemoryCache.Default;
            cacheItem.Add(cacheName, collection, policy);

            return collection;
        }
    }    
}