using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LFC.Model;
using Newtonsoft.Json;
using LFC.Business.Server;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LFC.WebSite.Controllers
{
    
    public class MenuController : Controller
    {
        private readonly IMenuServer _MenuServer;
        public MenuController(IMenuServer menuServer)
        {
            _MenuServer = menuServer;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Work()
        {
            JsonResult workMenu = new JsonResult(_MenuServer.GetMenus(1));
            return workMenu;
        }
        [HttpPost]
        public IActionResult Life()
        {
            JsonResult workMenu = new JsonResult(_MenuServer.GetMenus(2));
            return workMenu;
        }
        [HttpPost]
        public IActionResult Study()
        {
            JsonResult workMenu = new JsonResult(_MenuServer.GetMenus(3));
            return workMenu;
        }
        [HttpPost]
        public IActionResult Game()
        {
            JsonResult workMenu = new JsonResult(_MenuServer.GetMenus(4));
            return workMenu;
        }
    }
}
