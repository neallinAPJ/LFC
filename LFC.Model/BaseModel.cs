using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LFC.Model
{
    public class BaseModel
    {
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
