using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam10.Business.Entities
{
    public class UnavailableDay
    {
        public int ExpertId { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public UnavailableDay( int expertId, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday)
        {
            ExpertId = expertId;
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
        }
        public UnavailableDay(int expertId)
        {
            ExpertId = expertId;
           
        }
        public UnavailableDay()
        {
            

        }

        public int GetId()
        {
            return ExpertId;
        }
        public bool GetMon()
        {
            return Monday;
        }
        public bool GetTue()
        {
            return Tuesday;
        }
        public bool GetWed()
        {
            return Wednesday;
        }
        public bool GetThu()
        {
            return Thursday;
        }
        public bool GetFri()
        {
            return Friday;
        }
        public bool GetSat()
        {
            return Saturday;
        }
        public bool GetSun()
        {
            return Sunday;
        }



    }
}
