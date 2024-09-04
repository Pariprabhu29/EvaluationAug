using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace EvaluationAug
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        protected void Event_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void  RefreshData()
        {
            DataTable dt = DBUtil.GetData();

            target.Text = dt.Rows[0]["TargetQty"].ToString();
            production.Text = dt.Rows[0]["ProdQty"].ToString();
            shortfall.Text = dt.Rows[0]["ShortfallQty"].ToString();
            rejection.Text = dt.Rows[0]["Rejection_Qty"].ToString();
            rework.Text = dt.Rows[0]["Rework_Qty"].ToString();
        }
    }
}