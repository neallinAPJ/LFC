using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LFC.Model.Commodity;
using LFC.Utility.Cache;
using LFC.Utility.Convert;
using LFC.Business.Server;
using LFC.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LFC.WebSite.Controllers
{
    public class CommodityController : Controller
    {
        private readonly ICacheService _cache;
        private readonly ICommodityServer _server;
        public CommodityController(ICacheService cache, ICommodityServer server)
        {
            _cache = cache;
            _server = server;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetCommodityList(CommodityModel data)
        {
            JsonResult commodityJsons = new JsonResult(_server.GetCommoditys());
            return commodityJsons;
        }
        [HttpPost]
        public IActionResult Modify(int? id)
        {
            if(id.HasValue&& id.Value!=0)
            {
                return PartialView("_Moodify",_server.GetCommodity(id.Value));
            }
            return PartialView("_Moodify");
        }
        public IActionResult Delete(int? id)
        {
            ResponseModel response = new ResponseModel();
            response.statusCode = "200";
            response.message = "success";
            return Json(response);
        }
        
        [HttpPost]
        public IActionResult Modified(CommodityModel data)
        {
            data.ImageData = _cache.Get(data.FileName).Object2Bytes();
            _server.AddCommodity(data);
            ResponseModel response = new ResponseModel();
            response.statusCode = "200";
            response.message = "success";
            return Json(response);
        }

    }
}
