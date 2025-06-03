using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Services;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        PersonServices _personsevices;

        public PersonController()
        {
            _personsevices = new PersonServices();  
        }
        [HttpPut("{id}")]
        public IActionResult CreateAndPost([FromBody] PersonModels person,int id)
        {
          var model =  _personsevices.UpdateAndPostPerson(id, person);
            if(model.Complete == false)
            {
                return BadRequest(model);
            }
            return Ok(model);
        }
    }
}
