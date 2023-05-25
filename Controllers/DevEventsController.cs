using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDevEvents.Entities;
using ApiDevEvents.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDevEvents.Controllers
{
    [ApiController]
    [Route("api/dev-events")]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;

        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Where(x => !x.IsDeteleted).ToList();
            return Ok(devEvents);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _context.DevEvents.Include(de => de.Speakers).FirstOrDefault(x => x.Id == id);
            
            if(devEvent == null)
            {
                return NotFound();
            }
            return Ok(devEvent);
        }

        [HttpPost]
        public IActionResult Post(DevEvents devEvent)
        {
            _context.DevEvents.Add(devEvent);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = devEvent.Id}, devEvent);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id,DevEvents input)
        {
            var devEvent = _context.DevEvents.FirstOrDefault(x => x.Id == id);
            
            if(devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(input.Description, input.Title, input.EndDate, input.StartDate);
            _context.DevEvents.Update(devEvent);
            _context.SaveChanges();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var devEvent = _context.DevEvents.FirstOrDefault(x => x.Id == id);
            
            if(devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeakers(Guid id, DevEventSpeaker speaker)
        {
            speaker.DevEventId = id;
            var devEvent = _context.DevEvents.Any(x => x.Id == id);
            if(!devEvent)
            {
                return NotFound();
            }
            _context.DevEventSpeakers.Add(speaker);
            _context.SaveChanges();
            return NoContent();
        }

    }
}