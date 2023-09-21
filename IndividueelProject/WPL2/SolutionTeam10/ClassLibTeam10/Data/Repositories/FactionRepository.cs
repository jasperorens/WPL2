using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam00.Data.Framework;

namespace ClassLibTeam10.Data.Repositories
{
    static class FactionRepository
    {
        static FactionRepository()
        {
            FactionList = new List<Faction>();
        }

        public static List<Faction> FactionList { get; set; }
        public static void Add(int factionId, string race, string factionName, string factionLeader, string factionStronghold)
        {
            Faction faction = new Faction();
            faction.FactionId = factionId;
            faction.Race = race;
            faction.FactionName = factionName;
            faction.FactionLeader = factionLeader;
            faction.FactionStronghold = factionStronghold;
            Add(faction);
        }

        private static void Add(Faction faction)
        {
            FactionList.Add(faction);
            FactionData factionData= new FactionData();
            InsertResult insertResult = factionData.Insert(faction);
        }
    }
}
