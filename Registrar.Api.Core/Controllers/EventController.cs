using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Registrar.Api.Core.Data;
using Registrar.Api.Services.Dto;
using Registrar.Api.Services.Dto.Events;
using Registrar.Api.Services.Interfaces;
using System.Diagnostics.Tracing;

namespace Registrar.Api.Core.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : BaseApiController
    {
        private readonly IEventService _eventService;
        private const string eventCreateSuccess = "Event created successfully";
        private const string eventUpdateSuccess = "Event updated successfully";
        private const string eventDeleteSuccess = "Event deleted successfully";
        public EventController(ILogger logger, IEventService eventService) : base(logger)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreate eventCreate)
        {
            try
            {
                var userId = "";
                var response = await _eventService.CreateEventDetails(userId, eventCreate);
                var objId = new ObjectId { Id= response.Id };
                var responseModel = new ApiObjResponse<ObjectId> { Message = eventCreateSuccess, Data = objId };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpPut]
        [Route("{eventId}")]
        public async Task<IActionResult> UpdateEvent(string eventId, [FromBody] EventUpdate eventUpdate)
        {
            try
            {
                var response = await _eventService.UpdateEventDetails(eventId, eventUpdate);
                var objId = new ObjectId { Id = response.Id };
                var responseModel = new ApiObjResponse<ObjectId> { Message = eventCreateSuccess, Data = objId };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpDelete]
        [Route("{eventId}")]
        public async Task<IActionResult> DisableEvent(string eventId)
        {
            try
            {
                var response = await _eventService.DisableEvents(eventId);
                var objId = new ObjectId { Id = response };
                var responseModel = new ApiObjResponse<ObjectId> { Message = eventDeleteSuccess, Data = objId };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetEventsSlimInfo([FromQuery] EventListFilter filter)
        {
            try
            {
                var response = await _eventService.GetEvents(filter);
                var responseModel = new ApiListResponse<EventList> { Message = eventCreateSuccess, Data = response };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

    }
}
