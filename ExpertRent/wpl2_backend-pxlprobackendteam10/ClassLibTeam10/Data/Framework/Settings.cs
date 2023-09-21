using System;

namespace ClassLibTeam10.Data.Framework
{
    static class Settings
    {
        public static string GetConnectionString()
        {
            // String connectionString = "Trusted_Connection = True";";
            string connectionString = "user id = PxlUserT10;";
            connectionString += "Password = PxlUserT10;";
            connectionString += $@"Server=10.128.4.7;";
            connectionString += $"Database=Db2023Team10";
            return connectionString;
        }
    }
}
