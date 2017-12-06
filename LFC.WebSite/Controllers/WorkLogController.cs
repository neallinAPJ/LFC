using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LFC.Model.WorkLog;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LFC.WebSite.Controllers
{
    public class WorkLogController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetWorkLogList()
        {
            List<WorkLog> workLogList = new List<WorkLog>();
            WorkLog workLog = new WorkLog();
            workLog.ID = 1;
            workLog.Title = "日常记录";
            workLog.Content = "Testsssss";
            workLog.CreateBy= workLog.UpdateBy = "Neal";
            workLog.CreateDate= workLog.UpdateDate = DateTime.Now.Date;
            workLog.ProjectName = "MTRExpress";
            workLogList.Add(workLog);
            JsonResult workLogJsons = new JsonResult(workLogList);
            return workLogJsons;
        }
        public IActionResult WorkWeekly()
        {
            return View();
        }
    }
}
