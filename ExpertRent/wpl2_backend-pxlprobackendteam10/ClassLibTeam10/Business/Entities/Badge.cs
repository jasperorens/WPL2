using System;
using System.Collections.Generic;
using ClassLibTeam10.Business;
using ClassLibTeam10.Business.Enumerators;

namespace ClassLibTeam10.Business
{
    public class Badge
    {
        private Dictionary<Badges, bool> userBadges = new Dictionary<Badges, bool>();
        private string username;
        public Badge(string username)
        {
            foreach (Badges badge in Enum.GetValues(typeof(Badges)))
            {
                userBadges.Add(badge, false);
            }

            this.username = username;
        }

        public void SetBadge(Badges badges, bool hasBadge)
        {
            userBadges.Add(badges, hasBadge);
        }

        public bool GetBadge(Badges badges)
        {
            return userBadges[badges];
        }

        public Dictionary<Badges, bool> GetBadges()
        {
            return userBadges;
        }

        public string GetUsername()
        {
            return this.username;
        }
    }
}