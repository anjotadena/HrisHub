using HrisHub.Dal;
using HrisHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrisHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ICommonRepository<Event> _repository;

        public EventsController(ICommonRepository<Event> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            var events = await _repository.GetAll();

            return events.Count == 0 ? NotFound() : Ok(events);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Event>> GetDetails(int id)
        {
            var @event = await _repository.GetDetails(id);

            return @event == null ? NotFound() : Ok(@event);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Event>> Create(Event @event)
        {
            var result = await _repository.Insert(@event);

            return result != null ? CreatedAtAction("GetDetails", new { id = @event.Id }, @event) : BadRequest();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Event>> Update(int id, Event @event)
        {
            var currentEvent = await _repository.GetDetails(id);

            if (currentEvent == null)
            {
                return NotFound();
            }

            var result = await _repository.Update(id, @event);

            return result != null ? CreatedAtAction("GetDetails", new { id = @event.Id }, @event) : BadRequest();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Event>> Delete(int id)
        {
            var currentEvent = _repository.GetDetails(id);

            if (currentEvent == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return NoContent();
        }
    }
}
