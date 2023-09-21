using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ClassLibTeam06.Business.Entities;
using ClassLibTeam10.Business.Entities;
using Newtonsoft.Json;
using ClassLibTeam10.Business.Enumerators;
using ClassLibTeam10.Data.Framework;
using Newtonsoft.Json.Linq;

namespace ClassLibTeam10.Data
{

    public class UserData : SqlServer
    {
        public UserData()
        {
            TableName = "UserInfo";
        }

        public string TableName { get; set; }

        public SelectResult Select()
        {
            return base.Select(TableName);
        }

        public InsertResult RegisterUserToDB(User user)
        {
            InsertResult result = new InsertResult();
            try
            {
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append($"Insert INTO UserInfo ");
                insertQuery.Append(
                    $"(name, username, firstname, street, streetnr, country, postalcode, province, telephone, email, password) VALUES ");
                insertQuery.Append(
                    $"(@name, @username, @firstname, @street, @streetnr, @country, @postalcode, @province, @telephone, @email, @password); ");
                using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
                {
                    insertCommand.Parameters.Add("@username", SqlDbType.VarChar).Value =
                        user.GetUsername();
                    insertCommand.Parameters.Add("@name", SqlDbType.VarChar).Value =
                        user.GetName();
                    insertCommand.Parameters.Add("@firstname", SqlDbType.VarChar).Value =
                        user.GetFirstName();
                    insertCommand.Parameters.Add("@street", SqlDbType.VarChar).Value =
                        user.GetStreet();
                    insertCommand.Parameters.Add("@streetnr", SqlDbType.VarChar).Value =
                        user.GetStreetNr();
                    insertCommand.Parameters.Add("@country", SqlDbType.VarChar).Value =
                        user.GetCountry();
                    insertCommand.Parameters.Add("@postalcode", SqlDbType.VarChar).Value =
                        user.GetPostalCode();
                    insertCommand.Parameters.Add("@province", SqlDbType.VarChar).Value =
                        user.GetProvince();
                    insertCommand.Parameters.Add("@telephone", SqlDbType.VarChar).Value =
                        user.GetTelephone();
                    insertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value =
                        user.GetEmail();
                    insertCommand.Parameters.Add("@password", SqlDbType.VarChar).Value =
                        user.GetPassword();

                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return result;
        }

        public InsertResult EmailCodeToDB(string code, string userEmail)
        {
            InsertResult result = new InsertResult();
            try
            {
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append($"Update UserInfo ");
                insertQuery.Append(
                    $"set passwordcode = @Code where email = @UserEmail");
                using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
                {
                    insertCommand.Parameters.Add("@Code", SqlDbType.VarChar).Value = code;
                    insertCommand.Parameters.Add("@UserEmail", SqlDbType.VarChar).Value = userEmail;

                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return result;
        }
        

        public SelectResult GetEmailCodeFromDB(string userEmail)
        {
            SelectResult result = new SelectResult();

            try
            {
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append($"Select passwordcode From UserInfo Where email = @UserEmail");
                using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
                {
                    insertCommand.Parameters.Add("@UserEmail", SqlDbType.VarChar).Value = userEmail;

                    result = Select(insertCommand);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return result;
        }


        public InsertResult UpdateUser(User user)
        {
            InsertResult result = new InsertResult();
            try 
            {
                string updateQuery = "UPDATE UserInfo SET name = @name, firstname = @firstname, street=@street, streetnr=@streetnr, country=@country, postalcode=@postalcode, province = @province, telephone=@telephone, email=@email WHERE username = @username";

                using (SqlCommand insertCommand = new SqlCommand(updateQuery))
                {

                    insertCommand.Parameters.Add("@name", SqlDbType.VarChar).Value =
                        user.GetName();
                    insertCommand.Parameters.Add("@firstname", SqlDbType.VarChar).Value =
                        user.GetFirstName();
                    insertCommand.Parameters.Add("@street", SqlDbType.VarChar).Value =
                        user.GetStreet();
                    insertCommand.Parameters.Add("@streetnr", SqlDbType.VarChar).Value =
                        user.GetStreetNr();
                    insertCommand.Parameters.Add("@country", SqlDbType.VarChar).Value =
                        user.GetCountry();
                    insertCommand.Parameters.Add("@province", SqlDbType.VarChar).Value =
                        user.GetProvince();
                    insertCommand.Parameters.Add("@telephone", SqlDbType.VarChar).Value =
                        user.GetTelephone();
                    insertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value =
                        user.GetEmail();
                    insertCommand.Parameters.Add("@username", SqlDbType.VarChar).Value =
                        user.GetUsername();
                    insertCommand.Parameters.Add("@postalcode", SqlDbType.VarChar).Value =
                        user.GetPostalCode();

                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return result;
        }
        

        public string GetUserByName(string username)
        {
            
            SelectResult result;
            string sql = "SELECT * FROM UserInfo WHERE username = @userid";
            
            using (SqlCommand insertCommand = new SqlCommand(sql.ToString()))
            {
                insertCommand.Parameters.Add("@userid", SqlDbType.VarChar).Value =
                    username;
                
                result = Select(insertCommand);
            }
            
            String s = JsonConvert.SerializeObject(result.DataTable);
            return s;
            
            
        }
        
        public string GetPassCode(string email)
        {
            string cmd = $"Select * from UserInfo WHERE email = @email";
            SelectResult result;
            using (SqlCommand insertCommand = new SqlCommand(cmd.ToString()))
            {
                insertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value =
                    email;
                
                result = Select(insertCommand);
            }
            
            String s = JsonConvert.SerializeObject(result.DataTable);
            return s;
        }
        
        public InsertResult ChangeData(PasswordCode passwordCode)
        {
            InsertResult result = new InsertResult();
            try
            {
                string updateQuery = "UPDATE UserInfo SET password = @password WHERE email = @email";
                using (SqlCommand insertCommand = new SqlCommand(updateQuery)) 
                { 
                    insertCommand.Parameters.Add("@password", SqlDbType.VarChar).Value = passwordCode.GetCode();
                            
                    insertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = passwordCode.GetEmail();
                    
                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex) 
            {
                Console.Write(ex.Message);
            }
            return result;
        }
        

        public bool PasswordSuccess(Email email)
        {
            string passCode = GetPassCode(email.GetEmail());
            JArray jsonArray = JArray.Parse(passCode);
            JObject jsonObject = (JObject)jsonArray[0];
            string passwordCode = (string)jsonObject["passwordcode"];
            return email.GetCodeEmail().Equals(passwordCode);
        }

        public string GetUserFullName(int id)
        {
            SqlConnection sqlcn = new SqlConnection(Settings.GetConnectionString());
            sqlcn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcn;
            cmd.CommandText = "Select name, firstname from UserInfo where id = @ID";
            cmd.Parameters.AddWithValue("@ID", id);
            SqlDataReader data = cmd.ExecuteReader();
            data.Read();
            string name = data.GetString(0);
            string firstname = data.GetString(1);
            sqlcn.Close();
            return $"{firstname} {name}";
        }
    }
}