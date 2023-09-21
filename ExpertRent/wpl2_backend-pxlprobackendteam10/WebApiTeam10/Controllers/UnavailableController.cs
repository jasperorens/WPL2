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
using ClassLibTeam10.Business.Enumerators;
using ClassLibTeam10.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using ClassLibTeam06.Data;

namespace WebApiTeam10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnavailableController : Controller
    {        

        [HttpPost]
        [Route("UpdateDays")]
        public void getDays([FromBody] JsonObject jsonObject)
        {
            UnavailableDay days = JsonConvert.DeserializeObject<UnavailableDay>(jsonObject.ToString());
            UnavailableData daydate = new UnavailableData();
            daydate.UpdateUserToAvailability(days);
        }

        [HttpGet]
        [Route("GetDaysByID")]
        public IActionResult GetDaysByID(int id)
        {
            UnavailableData unavailableData = new UnavailableData();
            
            return Ok(unavailableData.GetUnavailableDay(id));
        }



    }
}
