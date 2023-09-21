using ClassLibTeam10.Business;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ClassLibTeam06.Business.Entities;
using ClassLibTeam06.Data;
using ClassLibTeam10.Business.Enumerators;
using ClassLibTeam10.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace WebApiTeam10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("UserJson")]
        //[FromBody] JObject user
        public void GetJson([FromBody] JsonObject json)
        {
            try
            {
                Console.Write(json.ToString());
                User register = JsonConvert.DeserializeObject<User>(json.ToString());
                addUser(register);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("ChangePassword")]
        public void ChangePassword([FromBody] JsonObject jsonObject)
        {
            Console.Write(jsonObject);
            UserData userData = new UserData();
            Email newData = JsonConvert.DeserializeObject<Email>(jsonObject.ToString());
            if (userData.PasswordSuccess(newData))
            {
                EmailData emailData = new EmailData();
                emailData.DeleteEmailCodeToDB(newData.GetEmail());
                PasswordCode passwordCode = new PasswordCode(newData.GetPassword(), newData.GetEmail());
                UserData changeData = new UserData();
                changeData.ChangeData(passwordCode);
            }
            
        }
        

        [HttpPost]
        [Route("AddUserViaSwagger")]
        public void addUserViaSwagger(
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
            string stateProvince)
        {
            User user = new User(email, password, username, name, firstname, telephone, country, street, streetNr, 
                postalCode, stateProvince);
            UserData userData = new UserData();
            userData.RegisterUserToDB(user);
        }

        [HttpPost]
        [Route("editUserInfo")]
        public void editUserInfo([FromBody] JsonObject json)
        {
            User register = JsonConvert.DeserializeObject<User>(json.ToString());
            UserData userData = new UserData();
            userData.UpdateUser(register);
        }   
        
        

        [HttpPost]
        [Route("AddUser")]
        public void addUser(User user)
        {
            UserData userData = new UserData();
            userData.RegisterUserToDB(user);
        }
        
        
        [HttpGet]
        [Route("GetUserByName")]
        public IActionResult GetTable(string username)
        {
            UserData userData = new UserData();
            return Ok(userData.GetUserByName(username));
        }


        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetEntireTable()
        {
            var users = Users.GetUsers();
            if (users.Succeeded)
            {
                var usersDataTable = users.DataTable;
                string userJSON = JsonConvert.SerializeObject(usersDataTable);
                return Ok(userJSON);
            }

            return NotFound();

        }
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUsers()
        {
            var user = new UserData();
            SelectResult result = user.Select();
            return Ok(result);
        }
        
        
    }

   
}
