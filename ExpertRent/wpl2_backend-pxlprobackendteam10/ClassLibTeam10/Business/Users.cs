
﻿using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data;
using ClassLibTeam10.Data.Framework;
using System.Diagnostics;
using System;

namespace ClassLibTeam10.Business
{
    public static class Users
    {
        public static SelectResult GetUsers()
        {

            UserData userData = new UserData();
            SelectResult result = userData.Select();
            return result;
        }
    }
}