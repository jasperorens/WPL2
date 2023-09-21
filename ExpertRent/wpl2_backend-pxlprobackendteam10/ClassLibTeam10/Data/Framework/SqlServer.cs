using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ClassLibTeam10.Data.Framework
{
    public class SqlServer
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        public SqlServer()
        {
            connection = new SqlConnection(Settings.GetConnectionString());
        }
        protected SelectResult Select(SqlCommand selectCommand)
        {
            var result = new SelectResult();
            try
            {
                using (connection)
                {
                    selectCommand.Connection = connection;
                    connection.Open();
                    adapter = new SqlDataAdapter(selectCommand);
                    result.DataTable = new DataTable();
                    adapter.Fill(result.DataTable);
                    connection.Close();
                }
                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }
        
        
        protected SqlDataReader SqlDataReader(String value)
        {
            string cmd = $"Select * from UserInfo WHERE username= @username";
                using (SqlCommand insertCommand = new SqlCommand(cmd, connection))
                { 
                    insertCommand.Parameters.Add($"@username", SqlDbType.VarChar).Value = 
                        value;
                    connection.Open();
                    SqlDataReader reader = insertCommand.ExecuteReader();
                    return reader;
                }
        }
        
        protected SqlDataReader SqlDataReaderEmail(String value)
        {
            string cmd = $"Select * from UserInfo WHERE email = @email";
            using (SqlCommand insertCommand = new SqlCommand(cmd, connection))
            { 
                insertCommand.Parameters.Add($"@email", SqlDbType.VarChar).Value = 
                    value;
                connection.Open();
                SqlDataReader reader = insertCommand.ExecuteReader();
                return reader;
            }
        }


        protected SqlDataReader SqlDataReaderID(String value, string table)
        {
            string cmd = $"Select * from {table} WHERE id= @id";
            using (SqlCommand insertCommand = new SqlCommand(cmd, connection))
            {
                insertCommand.Parameters.Add($"@id", SqlDbType.VarChar).Value =
                    value;
                connection.Open();
                SqlDataReader reader = insertCommand.ExecuteReader();
                return reader;
            }
        }

        protected SelectResult SqlCombinedMultiTableSelection()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = $"SELECT * from UserInfo INNER JOIN ExpertUserInfo ON UserInfo.id = ExpertUserInfo.id WHERE ExpertUserInfo.id = UserInfo.id AND ExpertUserInfo.active = 1";
            return Select(command);

        }
        
        protected SelectResult SqlCombinedSelection(String value)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = $"SELECT * from UserInfo INNER JOIN ExpertUserInfo ON UserInfo.id = ExpertUserInfo.id WHERE UserInfo.id = @id AND ExpertUserInfo.id = @id"; ;
            command.Parameters.Add($"@id", SqlDbType.VarChar).Value = value;
            return Select(command);
        }

        protected SelectResult Select(string tableName)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = $"SELECT * FROM {tableName}";
            return Select(command);
        }


        protected InsertResult InsertRecord(SqlCommand insertCommand) 
        {
            InsertResult result = new InsertResult();
            try
            {
                using(connection)
                {
                    insertCommand.Connection = connection; 
                    connection.Open(); 
                    insertCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
        protected SqlConnection GetConnectionString()
        {
            return connection;
        }

    }
}
