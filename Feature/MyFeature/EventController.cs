using System.ComponentModel.Design;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFeature.Feature.MyFeature
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Контроллер, отвечающий за операции с мероприятиями")]
    public class EventController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<MyFeatureController>
        [HttpGet("Get")]
        [SwaggerOperation(Summary = "Get all events", Description = "Get a list of all Events")]
        [SwaggerResponse(200, "OK", typeof(IEnumerable<List<Events>>))]
        [SwaggerResponse(400, "Validation failed", null)]
        public async Task<IActionResult> Get()
        {
            var request = new GetAllEvent.Request() { };
            var result = _mediator.Send(request);

            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Validation failed");
            }
        }

        // GET api/<MyFeatureController>/5
        [HttpGet("Get/{id}")]
        [SwaggerOperation(Summary = "Get all events", Description = "Get a list of all Events")]
        [SwaggerResponse(200, "OK", typeof(IEnumerable<List<Events>>))]
        [SwaggerResponse(400, "Validation failed", null)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetEvent.Request
            {
                eventId = id
            };
            var result = _mediator.Send(request);

            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Validation failed");
            } 
            //return events.Find(i => i.idEvent == id);
        }

        // POST api/<MyFeatureController>
        [HttpPost("Post")]
        [SwaggerOperation(Summary = "Add Event", Description = "Adding new Event in List")]
        [SwaggerResponse(200, "OK", null)]
        [SwaggerResponse(400, "Validation failed", null)]
        public async Task<IActionResult> Post([FromBody] Events newEvent)
        {
            var command = new EventMediatRValidation.Command
            {
                chgEvent = newEvent,
                AddEvent = true,
            };
            var result = _mediator.Send(command);

            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Validation failed");
            }

        }

        // PUT api/<MyFeatureController>
        [HttpPut("Put")]
        [SwaggerOperation(Summary = "Change Event", Description = "Change Event by id")]
        //[SwaggerParameter()]
        [SwaggerResponse(200, "OK", typeof(IEnumerable<List<Events>>))]
        [SwaggerResponse(400, "Validation failed", null)]
        public async Task<IActionResult> Put([FromBody] Events editEvent)
        {
            var command = new EventMediatRValidation.Command
            {
                eveId = editEvent.idEvent,
                chgEvent = editEvent,
                ChangeEvent = true,
            };
            var result = _mediator.Send(command);

            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Changing event failed");
            }
        }

        // DELETE api/<MyFeatureController>/5
        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(Summary = "Delete Event", Description = "Delete Event by id")]
        [SwaggerResponse(200, "OK", null )]
        [SwaggerResponse(400, "Validation failed", null)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new EventMediatRValidation.Command
            {
                eveId = id,
                DeleteEvent = true,
            };
            var result = _mediator.Send(command);

            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Deleting event failed");
            }
        }
    }
}
