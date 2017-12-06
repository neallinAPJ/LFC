using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LFC.Model.Commodity
{
    public class CommodityModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
    }
}
