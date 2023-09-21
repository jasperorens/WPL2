using ClassLibTeam00.Data.Framework;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam10.Business
{
    public static class Factions
    {
        public static IEnumerable<Faction> List()
        {
            return FactionRepository.FactionList;
        }
        public static void Add(int factionId, string race, string factionName, string factionLeader, string factionStronghold)
        {
            FactionRepository.Add(factionId, race, factionName, factionLeader, factionStronghold);
        }

        public static SelectResult GetFactions()
        {
            FactionData factionData = new FactionData();
            return factionData.Select();
        }
    }
}
