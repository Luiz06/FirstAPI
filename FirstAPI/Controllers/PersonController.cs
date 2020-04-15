using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAPI.Model;
using FirstAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        // GET: api/Person
        [HttpGet]
        public IActionResult Get()
        {
            List<Person> person = _personService.FindAll();

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
            var person = _personService.FindById(id);
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
            return Ok(_personService.Create(person));
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_personService.Update(person));
        }
    

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);

            return NoContent();
        }
    }
}
