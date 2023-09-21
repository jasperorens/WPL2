using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data;
using ClassLibTeam10.Data.Framework;
using ClassLibTeam10.Data.Repositories;
using System.Collections.Generic;
using System.Text;

namespace ClassLibTeam10.Business
{
    public class UnavailableDays
    {
        public static SelectResult GetUnavailableDays()
        {
            UnavailableData unavailableData = new UnavailableData();
            SelectResult result = unavailableData.Select();
            return result;
        }

    }
}
