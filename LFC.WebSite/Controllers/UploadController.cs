using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LFC.Utility.Cache;
using LFC.Model.Upload;
using LFC.Utility.Convert;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LFC.WebSite.Controllers
{
    public class UploadController : Controller
    {
        private readonly ICacheService _cache;
        public UploadController(ICacheService cache)
        {
            _cache = cache;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ImageUpload(string id, string name, string type, string lastModifiedDate, int size)
        {
            var file = Request.Form.Files;
            if (!_cache.Exists(name))
            {
                _cache.Add(name, file[0].GetFileData());
            }
            ImageModel image = new ImageModel();
            image.StatusCode = "200";
            image.filename = name;
            return Json(image);
        }
    }
}
