using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ClassLibTeam10.Data.Framework;

namespace ClassLibTeam10.Business.Entities {
    public class User
    {
        private string Email { get; set; }
        private string Password { get; set; }
        private string Username { get; set; }
        private string Name { get; set; }
        private string Firstname { get; set; }
        private string Telephone { get; set; }
        private string Country { get; set; }
        private string Street { get; set; }
        private string StreetNr { get; set; }
        private string PostalCode { get; set; }
        private string Province { get; set; }

        public User(
            string email,
            string password,
            string username,
            string name,
            string firstname,
            string telephone,
            string country,
            string street,
            string streetNr,
            string postalCode,
            string province)
        {
            Email = email;
            Password = password;
            Username = username;
            Name = name;
            Firstname = firstname;
            Telephone = telephone;
            Country = country;
            Street = street;
            StreetNr = streetNr;
            PostalCode = postalCode;
            Province = province;
        }


        public string GetUsername()
        {
            return Username;
        }

        public string GetFirstName()
        {
            return Firstname;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetPassword()
        {
            return Password;
        }

        public string GetEmail()
        {
            return Email;
        }

        public string GetTelephone()
        {
            return Telephone;
        }

        public string GetCountry()
        {
            return Country;
        }

        public string GetStreet()
        {
            return Street;
        }

        public string GetStreetNr()
        {
            return StreetNr;
        }

        public string GetPostalCode()
        {
            return PostalCode;
        }
        public string GetProvince()
        {
            return Province;
        }


        public void SetUsername(string username)
        {
            Username = username;
        }

        public void SetFirstName(string firstname)
        {
            Firstname = firstname;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetTelephone(string telephone)
        {
            Telephone = telephone;
        }

        public void SetCountry(string country)
        {
            Country = country;
        }

        public void SetStreet(string street)
        {
            Street = street;
        }

        public void SetStreetNr(string streetnr)
        {
            StreetNr  = streetnr;
        }

        public void SetPostalCode(string postalcode)
        {
            PostalCode = postalcode;
        }

        public void SetProvince( string province)
        {
            Province = province;
        }
    }
}