using ClassLibTeam00.Data.Framework;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System;
using ClassLibTeam10.Business.Entities;

internal class FactionData : SqlServer
{
    public FactionData()
    {
        TableName = "factions";
    }
    public string TableName { get; set; }
    public SelectResult Select()
    {
        return base.Select(TableName);
    }

    public InsertResult Insert(Faction faction)
    {
        var result = new InsertResult();
        try
        {
            StringBuilder insertQuery = new StringBuilder();
            insertQuery.Append($"Insert INTO {TableName} ");
            insertQuery.Append($"(factionId, race, factionName, factionLeader, factionStronghold) VALUES ");
            insertQuery.Append($"(@factionId, @race, @factionName, @factionLeader, @factionStronghold); ");
            using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
            {
                insertCommand.Parameters.Add("@factionId", SqlDbType.Int).Value =
                    faction.FactionId;
                insertCommand.Parameters.Add("@race", SqlDbType.VarChar).Value =
                    faction.Race;
                insertCommand.Parameters.Add("@factionName", SqlDbType.VarChar).Value =
                    faction.FactionName;
                insertCommand.Parameters.Add("@factionLeader", SqlDbType.VarChar).Value =
                    faction.FactionLeader;
                insertCommand.Parameters.Add("@factionStronghold", SqlDbType.VarChar).Value =
                    faction.FactionStronghold;
                result = InsertRecord(insertCommand);
            }
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message, ex);
        }
        return result;
    }
}