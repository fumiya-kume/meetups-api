using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using meetupsApi.JsonEntity;
using meetupsApi.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace meetupsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly meetupsApiContext _context;

        public EventsController(meetupsApiContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public IEnumerable<ConnpassEvent> GetEvent()
        {
            return _context.Event;
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] ConnpassEvent connpassEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != connpassEvent.event_id)
            {
                return BadRequest();
            }

            _context.Entry(connpassEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] ConnpassEvent connpassEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Event.Add(connpassEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = connpassEvent.event_id }, connpassEvent);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return Ok(@event);
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.event_id == id);
        }

        [HttpGet("Refresh")]
        public async Task<IActionResult> RefreshEventListAsync()
        {
            for (int i = 0; i < 1; i++)
            {
                using (var client = new HttpClient())
                {
                    var url = $"https://connpass.com/api/v1/event/?start={1 + i * 100}";
                    var result =  await client.GetAsync(url);
                    var json = await result.Content.ReadAsStringAsync();
                    var jsonResult = JsonConvert.DeserializeObject<ConnpassMeetupJson>(json);
                    jsonResult.ConnpassEvents.ToList().ForEach(eventEntity => {

                        _context.Add(eventEntity);
                        });
                    this._context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}