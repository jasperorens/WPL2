using ClassLibTeam10.Business.Entities;

namespace ClassLibTeam06.Business.Entities
{
    public class PasswordCode
    {
        private readonly string Code;
        private readonly string Email;

        public PasswordCode(string Code, string email)
        {
            this.Code = Code;
            this.Email = email;
        }

        public string GetCode()
        {
            return Code;
        }

        public string GetEmail()
        {
            return Email;
        }
    }
}