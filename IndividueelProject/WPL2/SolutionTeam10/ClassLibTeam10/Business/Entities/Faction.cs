﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam10.Business.Entities
{
    public class Faction
    {
        public int FactionId { get; set; }
        public string Race { get; set; }
        public string FactionName { get; set; }

        public string FactionLeader { get; set; }

        public string FactionStronghold { get; set; }
    }
}
