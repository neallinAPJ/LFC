using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LFC.Model
{
    public class Menu
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Target { get; set; }
        public string Url { get; set; }
        public List<Menu> Children { get; set; }
    }
}
