using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationAug
{
    public class SPCReport
    {
        public string Date { get; set; }
        public string Shift { get; set; }
        public string Component{ get; set; }
        public string SerialNo{ get; set; }

        public List<DynamicColHeaders> DynamicColHeaders { get; set; }
        public string SpindleLoad{ get; set; }
        public string Result{ get; set; }
        public string Remarks{ get; set; }


    }
}