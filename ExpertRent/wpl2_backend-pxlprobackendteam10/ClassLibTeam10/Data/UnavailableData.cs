using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Business.Enumerators;
using ClassLibTeam10.Data.Framework;
using Newtonsoft.Json;

namespace ClassLibTeam10.Data
{
    public class UnavailableData : SqlServer
    {
        public UnavailableData()
        {
            TableName = "UnavailableDays";
        }
        public string TableName { get; set; }

        public SelectResult Select()
        {
            return base.Select(TableName);
        }

        public SelectResult SelectMultiTable(string value)
        {
            return base.SqlCombinedSelection(value);
        }

        public SelectResult SelectAllMultiTable()
        {
            return base.SqlCombinedMultiTableSelection();
        }

        public SelectResult Select(string tableName)
        {
            return base.Select(tableName);
        }

        //Set UnavailableDays(Expert expert)
        //
        public InsertResult AddUserToAvailability(int IdOfExpert)
        {
            InsertResult result = new InsertResult();
            try
            {
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append($"Insert INTO UnavailableDays ");
                insertQuery.Append(
                    $"(ExpertId) VALUES ");
                insertQuery.Append(
                    $"(@ExpertId); ");
                using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
                {
                    insertCommand.Parameters.Add("@ExpertId", SqlDbType.Int).Value =
                        IdOfExpert;

                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return result;
        }

        public InsertResult UpdateUserToAvailability(UnavailableDay day)
        {
            InsertResult result = new InsertResult();
            try
            {
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append($"Update UnavailableDays ");
                insertQuery.Append(
                    $"set Monday = @monday , Tuesday = @tuesday , Wednesday = @wednesday , Thursday = @thursday , Friday = @friday , Saturday = @saturday , Sunday = @sunday Where ExpertId = @expertid");
                using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
                {
                    insertCommand.Parameters.Add("@monday", SqlDbType.Int).Value = day.GetMon();
                    insertCommand.Parameters.Add("@tuesday", SqlDbType.Int).Value = day.GetTue();
                        insertCommand.Parameters.Add("@wednesday", SqlDbType.Int).Value = day.GetWed();
                        insertCommand.Parameters.Add("@thursday", SqlDbType.Int).Value = day.GetThu();
                        insertCommand.Parameters.Add("@friday", SqlDbType.Int).Value = day.GetFri();
                        insertCommand.Parameters.Add("@saturday", SqlDbType.Int).Value = day.GetSat();
                        insertCommand.Parameters.Add("@sunday", SqlDbType.Int).Value = day.GetSun();
                        insertCommand.Parameters.Add("@expertid", SqlDbType.Int).Value = day.GetId();

                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return result;
        }

        public UnavailableDay GetUnavailableDay(int id)
        {
            SqlConnection sqlcn = new SqlConnection(Settings.GetConnectionString());
            sqlcn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcn;
            cmd.CommandText = "Select * from UnavailableDays where ExpertId = @ID";
            cmd.Parameters.AddWithValue("@ID", id);
            SqlDataReader data = cmd.ExecuteReader();
            data.Read();
            UnavailableDay day = new UnavailableDay();
            day.ExpertId = data.GetInt32(0);
            day.Monday = data.GetBoolean(1);
            day.Tuesday = data.GetBoolean(2);
            day.Wednesday = data.GetBoolean(3);
            day.Thursday = data.GetBoolean(4);
            day.Friday = data.GetBoolean(5);
            day.Saturday = data.GetBoolean(6);
            day.Sunday = data.GetBoolean(7);
            sqlcn.Close();
            return day;
        }



    }
}
