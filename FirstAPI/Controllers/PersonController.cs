using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAPI.Model;
using FirstAPI.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{

    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
   
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness personBusiness;

        public PersonController(IPersonBusiness personbusiness)
        {
            personBusiness = personbusiness;
        }


        // GET: api/Person
        [HttpGet]
        public IActionResult Get()
        {
            List<Person> person = personBusiness.FindAll();

            if(person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // GET: api/Person/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var person = personBusiness.FindById(id);
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST: api/Person
        [HttpPost]
        public IActionResult Post([FromBody] Person  person)
        {
            
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(personBusiness.Create(person));
        }

        // PUT: api/Person/5
        [HttpPut]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(personBusiness.Update(person));
        }
    

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            personBusiness.Delete(id);

            return NoContent();
        }
    }
}
