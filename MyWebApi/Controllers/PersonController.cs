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

        [HttpGet]
        public IActionResult GetPerson()
        {
           var res = _personsevices.GetPersonList();
            return Ok(res);
        }

        [HttpGet("id")]
        public IActionResult GetPersonById(int id) { 
        var res =_personsevices.GetPersonListById(id);
        if(res.Complete == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonModels person)
        {
           var res = _personsevices.PostPerson(person);
           if(res.Complete == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
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

        [HttpPatch("id")]

        public IActionResult Update([FromBody]PersonModels person, int id)
        {
            var model = _personsevices.UpdatePerson(id, person);
            if(model.Complete == false)
            {
                return BadRequest(model);
            }
            return Ok(model);
        }

        [HttpDelete("id)")]
        public IActionResult DeletePerson(int id)
        {
            ResponseModel res = _personsevices.DeletePerson(id);
            if(res.Complete == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
