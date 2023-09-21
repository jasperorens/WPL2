using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data;
using ClassLibTeam10.Data.Framework;
using ClassLibTeam10.Data.Repositories;
using System.Collections.Generic;
using ClassLibTeam06.Data;
using Newtonsoft.Json;

namespace ClassLibTeam10.Business
{
    public static class Experts
    {
        public static SelectResult GetExperts()
        {

            ExpertData expertData = new ExpertData();
            SelectResult result = expertData.Select();
            return result;
        }
        

        public static SelectResult GetUserExpert(string value)
        {
            ExpertData expertData = new ExpertData();
            SelectResult result = expertData.SelectMultiTable(value);
            return result;
        }
        
        public static SelectResult GetAllUserExperts()
        {
            ExpertData expertData = new ExpertData();
            SelectResult result = expertData.SelectAllMultiTable();
            return result;
        }

    }
}