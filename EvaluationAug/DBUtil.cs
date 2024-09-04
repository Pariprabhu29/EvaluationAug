using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;

namespace EvaluationAug
{
    public class DBUtil
    {
        public static DataTable GetData()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["screen1"].ConnectionString);
            SqlCommand command = new SqlCommand("S_DayWiseTargetAndonSaveAndView_KTA", connection);
            command.Parameters.AddWithValue("@Date", "2024-04-10 06:00:00");
            command.Parameters.AddWithValue("@Param", "DayWiseTargetAndonView");
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                dt.Load(dr);

            }
            catch (Exception ex)
            {
                new CustomException(ex.Message);
            }
            finally { connection?.Close(); };
            return dt;
        }

        public static List<object> GetDataForPieChart()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["screen3"].ConnectionString);
            SqlCommand command = new SqlCommand("s_GetSONA_ShiftAgg_DowntimeMatrix", connection);
            SqlDataReader dr = default;
            command.Parameters.AddWithValue("@StartTime", "2024-04-01");
            command.Parameters.AddWithValue("@EndTime", "2024-07-01");
            command.Parameters.AddWithValue("@MatrixType", "DLoss_By_Catagory");
            command.CommandType = CommandType.StoredProcedure;

            List<object> data = new List<object>();
            try
            {
                connection.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new { name = dr["DownID"].ToString(), y = Convert.ToDouble(dr["DownTimeInSeconds"]) });
                }

            }
            catch (Exception ex)
            {
                new CustomException(ex.Message);
            }
            finally { dr?.Close(); connection?.Close(); };
            return data;
        }

        private static List<string> GeneralExecution(string commandText, params SqlParameter[] parameter)
        {
            var results = new List<string>();
            results.Add("Select");
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["screen2"].ConnectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.Parameters.AddRange(parameter);
                command.CommandType = CommandType.Text;

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader[0].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    new CustomException(ex.Message);
                }
            }

            return results;
        }
        internal static List<string> GetMachineIDs()
        {
            return GeneralExecution(
                "select MachineID from Machineinformation"
            );
        }

        internal static List<string> GetComponentIDs(string mid)
        {
            return GeneralExecution(
                "select componentid from componentoperationpricing where MachineID=@MachineID",
                new SqlParameter[] { new SqlParameter("@MachineID", mid) }
            );
        }

        internal static List<string> GetOperationIDs(string cid)
        {
            return GeneralExecution(
                "select operationno from componentoperationpricing where componentid=@ComponentID",
                 new SqlParameter[] { new SqlParameter("@ComponentID", cid) }
            );
        }

        internal static List<string> GetCharacteristicIDs(string mid, string cid, string oid)
        {
            return GeneralExecution(
                "select CharacteristicCode from SPC_Characteristic where MachineID=@MachineID and componentid=@ComponentID and OperationNo=@OperationNo;",
                 new SqlParameter[] { new SqlParameter("@MachineID", mid), new SqlParameter("@ComponentID", cid), new SqlParameter("@OperationNo", oid) }
            );
        }

        internal static DataTable GetListview(string[] parameters)
        {
            DataTable dataTable = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["screen2"].ConnectionString))
            using (var command = new SqlCommand("S_GetSlnoWiseSPCReport_Bajaj", connection))
            {
                command.Parameters.AddWithValue("@MachineID", parameters[0] );
                command.Parameters.AddWithValue("@ComponentID", parameters[1]);
                command.Parameters.AddWithValue("@StartDate", parameters[2]);
                command.Parameters.AddWithValue("@EndDate", parameters[3]);
                command.Parameters.AddWithValue("@OperationNo", parameters[4]);
                command.Parameters.AddWithValue("@Status",parameters[5]);
                command.Parameters.AddWithValue("@Dimension",parameters[6]);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    new CustomException(ex.Message);
                }
            }
            return dataTable;
        }

        
    }
}












