namespace ClassLibTeam06.Business.Entities
{
    public class EmailCode
    {
        private readonly string Email;

        public EmailCode(string email)
        {
            Email = email;
        }

        public string GetEmail()
        {
            return Email;
        }
    }
}