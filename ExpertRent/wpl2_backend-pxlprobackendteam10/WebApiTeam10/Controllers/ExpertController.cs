using System.Text.Json.Nodes;
using ClassLibTeam06.Data;
using ClassLibTeam10.Business;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApiTeam10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpertController : ControllerBase
    {
        [HttpPost]
        [Route("ExpertJson")]
        public void AddExpert([FromBody] JsonObject jsonObject)
        {
            try
            {
                Expert register = JsonConvert.DeserializeObject<Expert>(jsonObject.ToString());
                ExpertData expertData = new ExpertData();
                expertData.RegisterUserAsExpert(register);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        
        // [HttpGet]
        // [Route("GetExpertById")]
        // public IActionResult GetExpert(int id)
        // {
        //     ExpertData expertData = new ExpertData();
        //     return Ok(expertData.GetExpertByName(id));
        // }

        [HttpGet]
        [Route("GetExpertById")]
        public IActionResult GetExpertById(string id)
        {
            ExpertData expertData = new ExpertData();
            return Ok(expertData.GetExpertById(id));
        }


        [HttpGet]
        [Route("GetExperts")]
        public IActionResult GetExpertTable()
        {
            var experts = Experts.GetExperts();
            if (experts.Succeeded)
            {
                var expertsDataTable = experts.DataTable;
                string expertsJSON = JsonConvert.SerializeObject(expertsDataTable);
                return Ok(expertsJSON);
            }

            return NotFound();
        }

        /*[HttpGet]
        [Route("GetExpertIfUser")]
        public IActionResult GetExpertByUser(string id)
        {
            var experts = Experts.GetUserExpert(id);
            if (experts.Succeeded)
            {
                var expertsDataTable = experts.DataTable;
                string expertsJSON = JsonConvert.SerializeObject(expertsDataTable);
                return Ok(expertsJSON);
            }

            return NotFound();
        }*/
        
        [HttpPost]
        [Route("AddExpertViaSwagger")]
        public void addExpertViaSwagger(
            string profession,
            string specialization,
            int id,
            string category,
            int price)
        {
            Expert expert = new Expert(profession, specialization, id, category, price, 1);
            ExpertData expertData = new ExpertData();;
            expertData.RegisterUserAsExpert(expert);
        }
        
        [HttpPost]
        [Route("editExpertInfo")]
        public void editExpertInfo([FromBody] JsonObject json)
        {
            Expert register = JsonConvert.DeserializeObject<Expert>(json.ToString());
            ExpertData expert = new ExpertData();
            expert.UpdateExpert(register);
        }

        [HttpPost]
        [Route("editExpertActive")]
        public void editExpertActive([FromBody] JsonObject json)
        {
            Expert register = JsonConvert.DeserializeObject<Expert>(json.ToString());
            ExpertData expert = new ExpertData();
            expert.UpdateExpertActive(register);
        }

        [HttpGet]
        [Route("GetAllExpertsIfUser")]
        public IActionResult GetAllExpertsByUser()
        {
            var experts = Experts.GetAllUserExperts();
            if (experts.Succeeded)
            {
                var expertsDataTable = experts.DataTable;
                string expertsJSON = JsonConvert.SerializeObject(expertsDataTable);
                return Ok(expertsJSON);
            }

            return NotFound();
        }
    }
}