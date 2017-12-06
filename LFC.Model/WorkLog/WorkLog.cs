using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LFC.Model.WorkLog
{
    public class WorkLog: BaseModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ProjectName { get; set; }
        public string Content { get; set; }

        
    }
}
