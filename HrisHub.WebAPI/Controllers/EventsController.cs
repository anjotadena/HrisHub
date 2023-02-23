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
        public ActionResult<IEnumerable<Event>> Get()
        {
            var events = _repository.GetAll();

            return events.Count == 0 ? NotFound() : Ok(events);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Event> Get(int id)
        {
            var @event = _repository.GetDetails(id);

            return @event == null ? NotFound() : Ok(@event);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Event> Create(Event @event)
        {
            _repository.Insert(@event);

            var result = _repository.SaveChanges();

            return result > 0 ? CreatedAtAction("GetDetails", new { id = @event.Id }, @event) : NotFound();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Event> Update(int id, Event @event)
        {
            var currentEvent = _repository.GetDetails(id);

            if (currentEvent == null)
            {
                return NotFound();
            }

            _repository.Update(@event);

            var result = _repository.SaveChanges();

            return result > 0 ? CreatedAtAction("GetDetails", new { id = @event.Id }, @event) : NotFound();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Event> Delete(int id)
        {
            var currentEvent = _repository.GetDetails(id);

            if (currentEvent == null)
            {
                return NotFound();
            }

            _repository.Delete(currentEvent);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
