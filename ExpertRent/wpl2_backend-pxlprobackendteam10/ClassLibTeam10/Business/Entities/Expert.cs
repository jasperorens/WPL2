using System;
using ClassLibTeam10.Data.Framework;

namespace ClassLibTeam10.Business.Entities
{
    public class Expert
    {
        private string Profession { get; set; }
        private string Specialization { get; set; }
        private string Category { get; set; }
        private float Price { get; set; }
        private int Id { get; set; }
        private int Active { get; set; }

        public Expert(
            string profession,
            string specialization,
            int id,
            string category,
            float price, 
            int active)
        {
            Profession = profession;
            Specialization = specialization;
            Category = category;
            Price = price;
            Id = id;
            Active = active;
        }


        public int GetId()
        {
            return Id;
        }
        public string GetProfession()
        {
            return Profession;
        }
        public string GetSpecialization()
        {
            return Specialization;
        }
        public string GetCategory()
        {
            return Category;
        }
        public float GetPrice()
        {
            return Price;
        }
        public int GetActive()
        {
            return Active;
        }
        public void SetId(int id)
        {
            Id = id;
        }
        public void SetProfession(string profession)
        {
            Profession = profession;
        }
        public void SetSpecialization(string specialization)
        {
            Specialization = specialization;
        }
        public void SetCategory(string category)
        {
            Category = category;
        }
        public void SetPrice(float price)
        {
            Price = price;
        }
        public void SetActive(int active)
        {
            Active = active;
        }

    }
}