using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LFC.DataProvider.Model
{
    [Table("Commodity")]
    public class Commodity
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
