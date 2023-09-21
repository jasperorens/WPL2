using Microsoft.AspNetCore.Mvc;
using ClassLibTeam10.Business;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace WebApiTeam10.Controllers
{
    //WebApi -> Add -> Project reference

    [ApiController]
    [Route("api/[controller]")]

    public class FactionController : ControllerBase
    {
        [HttpPost]
        [Route("AddFaction")]

        public void Addcard(int factionId, string race, string factionName, string factionLeader, string factionStronghold)
        {
            //voeg dependincy van lib toe
            Factions.Add(factionId, race, factionName, factionLeader, factionStronghold);
        }
        [HttpGet]
        [Route("GetFactions")]

        [HttpGet]
        public ActionResult GetAllFactions()
        {
            var result = Factions.GetFactions();
            if (result.Succeeded)
            {
                var factions = result.DataTable;
                string JSONresult = JsonConvert.SerializeObject(factions);
                return Ok(JSONresult);
            }
            return NotFound();
        }
    }

}
