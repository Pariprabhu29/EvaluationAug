using DotNet.Highcharts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace EvaluationAug
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        int numberofdynamiccols;
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            if (!IsPostBack)
            {
                machine.DataSource = DBUtil.GetMachineIDs();
                machine.ClearSelection();
                machine.DataBind();

            }
        }

        protected void machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            component.DataSource = DBUtil.GetComponentIDs(machine.SelectedValue);
            component.DataBind();
        }

        protected void component_SelectedIndexChanged(object sender, EventArgs e)
        {
            operation.DataSource = DBUtil.GetOperationIDs(component.SelectedValue);
            operation.DataBind();
        }

        protected void operation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = DBUtil.GetCharacteristicIDs(machine.SelectedValue, component.SelectedValue, operation.SelectedValue);
            if (list.Count > 1)
            {
                characteristic.DataSource = list;
            }
            else
            {
            }
            characteristic.DataBind();
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            StringBuilder dynamicColsBuilder = new StringBuilder();
            foreach (var idx in characteristic.GetSelectedIndices())
            {
                dynamicColsBuilder.Append(characteristic.Items[idx].Value).Append(",");
            }
            string dynamicCols = dynamicColsBuilder.ToString().TrimEnd(',');

            StringBuilder statusesBuilder = new StringBuilder();
            foreach (var idx in status.GetSelectedIndices())
            {
                statusesBuilder.Append(status.Items[idx].Value).Append(",");
            }
            string statuses = statusesBuilder.ToString().TrimEnd(',');
            dt= DBUtil.GetListview(new string[] { machine.SelectedValue, component.SelectedValue, fromDate.Value, toDate.Value, operation.SelectedValue, statuses, dynamicCols });

            LV1.DataSource = returnListOfSPCReport(dt);
            LV1.DataBind();
        }
        protected List<SPCReport> returnListOfSPCReport(DataTable dt)
        {
            var list = new List<SPCReport>();
            numberofdynamiccols = dt.Columns.Count - 10;

            SPCReport report0 = new SPCReport()
            {
                Date = "Date",
                Component = "Component",
                Shift = "Shift",
                SerialNo = "Serial No",
                SpindleLoad = "Spindle Load",
                DynamicColHeaders = GetDynamicHeaders(dt),
                Result = "Result",
                Remarks = "Remarks",

            };
            list.Add(report0);

            if (dt.Rows.Count > 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    SPCReport report = new SPCReport()
                    {
                        Date = row["Date"].ToString(),
                        Component = row["ComponentID"].ToString(),
                        Shift = row["Shift"].ToString(),
                        SerialNo = row["SerialNo"].ToString(),
                        SpindleLoad = row["SpindleLoad"].ToString(),
                        DynamicColHeaders = GetDynamicValues(row, numberofdynamiccols),
                        Result = row["Result"].ToString(),
                        Remarks = row["Remarks"].ToString(),

                    };
                    list.Add(report);
                }
            }
            return list;
        }
        private static List<DynamicColHeaders> GetDynamicValues(DataRow row, int numberofdynamiccols)
        {
            List<DynamicColHeaders> values = new List<DynamicColHeaders>();
            for (int i = 0; i < numberofdynamiccols; i++)
            {
                DynamicColHeaders datavalues = new DynamicColHeaders()
                {
                    ColSubHeaders = row[i + 10].ToString(),
                };
                values.Add(datavalues);
            }
            return values;
        }

        private List<DynamicColHeaders> GetDynamicHeaders(DataTable dt)
        {
            List<DynamicColHeaders> headers = new List<DynamicColHeaders>();
            for (int i = 10; i < dt.Columns.Count; i++)
            {
                DynamicColHeaders dynamicColHeaders = new DynamicColHeaders()
                {
                    ColSubHeaders = dt.Columns[i].ColumnName,
                };
                headers.Add(dynamicColHeaders);
            }
            return headers;


        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            //FileInfo fileinfo = new FileInfo(@"C:/Users/devteam/Desktop/TemplateForMachine.xls");
            //if (fileinfo.Exists)
            //{
            //    using (var package = new ExcelPackage(fileinfo))
            //    {
            //        var sheet = package.Workbook.Worksheets[0];
            //        int ColtoInsertFrom = 7;
            //        sheet.InsertColumn(ColtoInsertFrom, numberofdynamiccols);
            //        int i;
            //        for (i = 0; i < numberofdynamiccols; i++)
            //        {
            //            var headersrange = sheet.Cells[2, ColtoInsertFrom + i];
            //            headersrange.Value = dt.Columns[10+i].ColumnName; 
            //            headersrange.Style.Border.BorderAround(ExcelBorderStyle.Thick);
            //            headersrange.Style.Font.Bold = true;
            //            sheet.Cells.AutoFitColumns();
            //        }
            //        //string columnletter = ExcelCellBase.GetAddressCol(ColtoInsertFrom + i - 1);
            //        //string address = columnletter + "2";
            //        //var range = sheet.Cells["J2:" + address];
            //        //range.Merge = true;
            //        //range.Value = "Loss";
            //        //range.Style.Font.Bold = true;
            //        //range.Style.Border.BorderAround(ExcelBorderStyle.Thick);
            //        //range.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;

            //        int row = 3;
            //        int slno = 1;
            //        int col = 0;
            //        foreach (DataRow dtrow in dt.Rows)
            //        {
            //            row++;
            //            sheet.Cells[row, 1].Value = slno++;
            //            DateTime date = Convert.ToDateTime(dtrow["Date"]);

            //            sheet.Cells[row, 2].Value = date.Date.ToString("dd-MM-yyyy");
            //            sheet.Cells[row, 3].Value = dtrow["MachineID"];
            //            sheet.Cells[row, 4].Value = dtrow["OperatorName"];
            //            sheet.Cells[row, 5].Value = dtrow["ShiftName"];
            //            sheet.Cells[row, 6].Value = dtrow["ComponentID"];
            //            sheet.Cells[row, 7].Value = dtrow["ComponentDescription"];
            //            sheet.Cells[row, 8].Value = dtrow["OperationNo"];
            //            sheet.Cells[row, 9].Value = dtrow["OpnDescription"];
            //            for (col = 0; col < coltoInsert; col++)
            //            {
            //                sheet.Cells[row, col + ColtoInsertFrom].Value = dtrow[col + OEENextColumnNo];
            //            }
            //            sheet.Cells[row, col++ + ColtoInsertFrom].Value = dtrow["AE"];
            //            sheet.Cells[row, col++ + ColtoInsertFrom].Value = dtrow["PE"];
            //            sheet.Cells[row, col++ + ColtoInsertFrom].Value = dtrow["QE"];
            //            sheet.Cells[row, col + ColtoInsertFrom].Value = dtrow["OEE"];
            //        }
            //        foreach (DataRow dtrow in dt2.Rows)
            //        {
            //            row++;
            //            for (col = 0; col <= LastColOrdinalInDB - OEENextColumnNo; col++)
            //            {
            //                sheet.Cells[row, col + ColtoInsertFrom].Value = dtrow[col];
            //            }
            //        }
            //        foreach (DataRow dtrow in dt3.Rows)
            //        {
            //            for (int x = 0; x < 4; x++, col++)
            //            {
            //                sheet.Cells[row, col + ColtoInsertFrom].Value = dtrow[x];
            //            }
            //        }
            //        sheet.Cells.AutoFitColumns();

            //        using (var memoryStream = new MemoryStream())
            //        {
            //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //            Response.AddHeader("content-disposition", "attachment;  filename=ExcelReport_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx");
            //            package.SaveAs(memoryStream);
            //            memoryStream.WriteTo(Response.OutputStream);
            //            Response.Flush();
            //            Response.Close();
            //        }
            //    }
            //}

        }
    }
}