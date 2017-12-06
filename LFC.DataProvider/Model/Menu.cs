using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace LFC.DataProvider.Model
{
    [Table("menu")]
    public class Menu
    {
        public int ID { get; set; }
        public int? PID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public string MenuType { get; set; }
        public string MenuID { get; set; }
    }
}
