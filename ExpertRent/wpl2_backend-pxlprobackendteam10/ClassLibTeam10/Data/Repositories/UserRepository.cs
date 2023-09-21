using System.Collections.Generic;
using System.Runtime.Remoting.Activation;
using ClassLibTeam10.Business.Entities;

namespace ClassLibTeam10.Data.Repositories
{
    static class UserRepository
    {
        static UserRepository()
        { 
            UserList = new List<User>();
        }
        

        public static List<User> UserList { get; set; }
        public static void Add(string email, string password, string username, string name, string firstname, string telephone, string country, string street, int streetnr, int postal, string province, string image)
        { 
            var user = new User(email,
                password,
                username,
                name,
                firstname,
                telephone,
                country,
                street,
                streetnr.ToString(),
                postal.ToString(),
                province
                
            );
            Add(user);
        }
        
        private static void Add(User user)
        {
            UserList.Add(user);
        }
    }
}