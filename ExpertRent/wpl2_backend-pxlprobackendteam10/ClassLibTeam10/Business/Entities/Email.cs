namespace ClassLibTeam10.Business.Entities
{
    public class Email
    {
        private readonly string emailcode;
        private readonly string email;
        private readonly string newpass;

        public Email(string emailcode, string email, string newpass)
        {
            this.emailcode = emailcode;
            this.email = email;
            this.newpass = newpass;
        }

        public string GetEmail()
        {
            return email;
        }
        
        public string GetCodeEmail()
        {
            return emailcode;
        }
        
        public string GetPassword()
        {
            return newpass;
        }
    }
}