using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EvaluationAug
{
    public partial class highchart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static List<object> GetDataForDisplayInPiechart()
        {

            return DBUtil.GetDataForPieChart();
        }
    }
}