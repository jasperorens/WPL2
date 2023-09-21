using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data.Framework;
using Newtonsoft.Json;

namespace ClassLibTeam10.Data
{
    public class BookingData : SqlServer
    {
        public BookingData()
        {
            TableName = "Bookings";
        }
        
        public string TableName { get; set; }
        public SelectResult Select()
        {
            return base.Select(TableName);
        }
        
        
        public SelectResult Select(string tableName)
        {
            return base.Select(tableName);
        }
        
        
        
        public InsertResult AddBooking(Booking booking)
        {
            InsertResult result = new InsertResult();
            try
            {
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append($"Insert INTO Bookings ");
                insertQuery.Append(
                    $"(idbuyer, idseller, date, starttime, endtime, paymentNum, amount, paymentMethod, paymentName) VALUES ");
                insertQuery.Append(
                    $"(@idbuyer, @idseller, @date, @starttime, @endtime, @paymentNum, @amount, @paymentMethod, @paymentName); ");
                using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
                {
                    insertCommand.Parameters.Add("@idbuyer", SqlDbType.VarChar).Value =
                        booking.GetIdBuyer();
                    insertCommand.Parameters.Add("@idseller", SqlDbType.VarChar).Value =
                        booking.GetIdSeller();
                    insertCommand.Parameters.Add("@date", SqlDbType.VarChar).Value =
                        booking.GetDate();
                    insertCommand.Parameters.Add("@starttime", SqlDbType.VarChar).Value =
                        booking.GetStartTime();
                    insertCommand.Parameters.Add("@endtime", SqlDbType.VarChar).Value =
                        booking.GetEndTime();
                    insertCommand.Parameters.Add("@paymentNum", SqlDbType.VarChar).Value =
                        booking.GetPaymentNum();
                    insertCommand.Parameters.Add("@amount", SqlDbType.VarChar).Value =
                        booking.GetAmount();
                    insertCommand.Parameters.Add("@paymentMethod", SqlDbType.VarChar).Value =
                        booking.GetPaymentMethod();
                    insertCommand.Parameters.Add("@paymentName", SqlDbType.VarChar).Value =
                        booking.GetPaymentName();
                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return result;
        }


        public string GetPaymentData(string id)
        {
            SelectResult result;
            string sql = "SELECT * FROM Bookings WHERE buyerid = @buyerid";
            
            using (SqlCommand insertCommand = new SqlCommand(sql.ToString()))
            {
                insertCommand.Parameters.Add("@buyerid", SqlDbType.VarChar).Value =
                    id;
                
                result = Select(insertCommand);
            }
            
            String s = JsonConvert.SerializeObject(result.DataTable);
            return s;
        }
        
        public string GetBookedDates(string id)
        {
            SelectResult result;
            string sql = "SELECT idbuyer, idseller, date, starttime, endtime FROM Bookings WHERE idseller = @idseller";
            
            using (SqlCommand insertCommand = new SqlCommand(sql.ToString()))
            {
                insertCommand.Parameters.Add("@idseller", SqlDbType.VarChar).Value =
                    id;
                
                result = Select(insertCommand);
            }
            
            String s = JsonConvert.SerializeObject(result.DataTable);
            return s;
        }

        List<string> hours = new List<string>();
        public string[] GetBookedHours(string id, string day)
        {
            
            SqlConnection sqlcn = new SqlConnection(Settings.GetConnectionString());
            sqlcn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcn;
            cmd.CommandText = "Select starttime, endtime from Bookings where idseller = @ID and date = @date";
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@date", day);
            SqlDataReader data = cmd.ExecuteReader();
            while(data.Read())
            {
                for(int i = 0 ; i < data.FieldCount; i++)
                {
                    LaatsteUurEnHoogsteUur(data.GetString(i),data.GetString(i + 1));
                    i++;
                }
            }
            vulUren();
            sqlcn.Close();
            return hours.ToArray();

        }
        int highest = 0;
        int lowest = 24;
        private void LaatsteUurEnHoogsteUur(string start, string end)
        {
            string[] veld1 = start.Split(':');
            string[] veld2 = end.Split(':');
            int starthour = Convert.ToInt32(veld1[0]);
            int endhour = Convert.ToInt32(veld2[0]);
            if(starthour < lowest)
            {
                lowest = starthour;
            }

            if(endhour > highest)
            {
                highest = endhour;
            }
        }

        private void vulUren()
        {
            for (int i = 0; i < 24; i++)
            {
                if(i < lowest)
                {
                    hours.Add($"{i}:00");
                }
            }
        }


    }
}