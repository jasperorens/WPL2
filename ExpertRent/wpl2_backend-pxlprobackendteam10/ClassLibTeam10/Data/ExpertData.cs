using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ClassLibTeam06.Data;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Business.Enumerators;
using ClassLibTeam10.Data.Framework;
using Newtonsoft.Json;

namespace ClassLibTeam10.Data {

    public class ExpertData : SqlServer
    {
        public ExpertData()
        {
            TableName = "ExpertUserInfo";
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
        
        
        
        public InsertResult RegisterUserAsExpert(Expert expert)
        {
            InsertResult result = new InsertResult();
            try
            {
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append($"Insert INTO ExpertUserInfo ");
                insertQuery.Append(
                    $"(profession, specialization, category, price, id) VALUES ");
                insertQuery.Append(
                    $"(@profession, @specialization, @category, @price, @id); ");
                using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
                {
                    insertCommand.Parameters.Add("@profession", SqlDbType.VarChar).Value =
                        expert.GetProfession();

                    insertCommand.Parameters.Add("@specialization", SqlDbType.VarChar).Value =
                        expert.GetSpecialization();
                    insertCommand.Parameters.Add("@category", SqlDbType.VarChar).Value =
                        expert.GetCategory();
                    insertCommand.Parameters.Add("@price", SqlDbType.Float).Value =
                        expert.GetPrice();
                    insertCommand.Parameters.Add("@id", SqlDbType.VarChar).Value =
                        expert.GetId();

                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
            UnavailableData date = new UnavailableData();
            date.AddUserToAvailability(expert.GetId());
            BadgeData badgeData = new BadgeData();
            badgeData.AddBadges(expert.GetId());

            return result;
        }

        
        
        
        public string GetExpertById(string id)
        {
            SelectResult result;
            string sql = "SELECT * FROM ExpertUserInfo WHERE id = @userid";
            
            using (SqlCommand insertCommand = new SqlCommand(sql.ToString()))
            {
                insertCommand.Parameters.Add("@userid", SqlDbType.VarChar).Value =
                    id;
                
                result = Select(insertCommand);
            }
            
            String s = JsonConvert.SerializeObject(result.DataTable);
            return s;
            
        }

        public InsertResult UpdateExpert(Expert expert)
        {
            InsertResult result = new InsertResult();
            try
            {
                string updateQuery = "UPDATE ExpertUserInfo SET profession = @profession, specialization = @specialization, category = @category, price = @price, active = @active WHERE id = @id";

                using (SqlCommand insertCommand = new SqlCommand(updateQuery))
                {

                    insertCommand.Parameters.Add("@profession", SqlDbType.VarChar).Value =
                        expert.GetProfession();

                    insertCommand.Parameters.Add("@specialization", SqlDbType.VarChar).Value =
                        expert.GetSpecialization();

                    insertCommand.Parameters.Add("@category", SqlDbType.VarChar).Value =
                        expert.GetCategory();

                    insertCommand.Parameters.Add("@price", SqlDbType.Float).Value =
                        expert.GetPrice();

                    insertCommand.Parameters.Add("@active", SqlDbType.Bit).Value =
                       expert.GetActive();

                    insertCommand.Parameters.Add("@id", SqlDbType.VarChar).Value =
                        expert.GetId();



                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return result;
        }
        public InsertResult UpdateExpertActive(Expert expert)
        {
            InsertResult result = new InsertResult();
            try
            {
                string updateQuery = "UPDATE ExpertUserInfo SET active = @active WHERE id = @id";

                using (SqlCommand insertCommand = new SqlCommand(updateQuery))
                {

                    insertCommand.Parameters.Add("@active", SqlDbType.Bit).Value =
                        expert.GetActive();
                    insertCommand.Parameters.Add("@id", SqlDbType.VarChar).Value =
                        expert.GetId();

                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return result;
        }



    }
}