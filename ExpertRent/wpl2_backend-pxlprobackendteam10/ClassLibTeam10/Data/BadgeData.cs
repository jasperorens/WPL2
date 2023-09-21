using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ClassLibTeam10.Business;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Business.Enumerators;
using ClassLibTeam10.Data.Framework;
using Newtonsoft.Json;

namespace ClassLibTeam06.Data
{
    public class BadgeData : SqlServer
    {
        public BadgeData()
        {
            TableName = "Badges";
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



        public InsertResult AddBadges(int id)
        {
            InsertResult result = new InsertResult();
            try
            {
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append($"Insert INTO Badges(userid) values(@userID)");

                SqlCommand insertCommand = new SqlCommand(insertQuery.ToString());

                    insertCommand.Parameters.Add("@userID", SqlDbType.SmallInt).Value = id;

                    result = InsertRecord(insertCommand);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return result;
        }


        public string GetBadgeFromUser(string id)
        {
            SelectResult result;
            string sql = "SELECT * FROM Badges WHERE userid = @userid";
            
            using (SqlCommand insertCommand = new SqlCommand(sql.ToString()))
            {
                insertCommand.Parameters.Add("@userid", SqlDbType.VarChar).Value =
                    id;
                
                result = Select(insertCommand);
            }
            
            String s = JsonConvert.SerializeObject(result.DataTable);
            return s;
        }
    }
}

