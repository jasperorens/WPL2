using System.Text.Json.Nodes;
using ClassLibTeam06.Data;
using ClassLibTeam10.Business;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApiTeam10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BadgeController : ControllerBase
{
    [HttpPost] // Gebruik post
    [Route("addBadge")] // api/booking/addbooking word opgeroepen
    public void addBadges([FromBody] int id) // FromBody word de Json gestoken
    {       
        BadgeData badgedata = new BadgeData();
        badgedata.AddBadges(id);
    }

    [HttpGet]
    [Route("GetBadge")]
    public IActionResult GetBadges(string id)
    {
        BadgeData badgeData = new BadgeData();
        string badges = badgeData.GetBadgeFromUser(id);
        Console.Write((JsonConvert.SerializeObject(badges)));
        return Ok(JsonConvert.SerializeObject(badges));

    }

}
