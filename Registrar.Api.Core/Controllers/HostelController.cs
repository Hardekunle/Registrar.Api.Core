using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using Registrar.Api.Core.Data;
using Registrar.Api.Services.Dto;
using Registrar.Api.Services.Dto.Accomodations;
using Registrar.Api.Services.Interfaces;
using System.Runtime.InteropServices;

namespace Registrar.Api.Core.Controllers
{
    [Route("api/hostel")]
    [ApiController]
    public class HostelController : BaseApiController
    {
        private readonly IAccommodationService _accommodationService;
        private const string hostelCreateSuccess = "";
        public HostelController(ILogger logger, IAccommodationService accommodationService) : base(logger)
        {
            _accommodationService = accommodationService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateHostel([FromBody] HostelCreate hostel)
        {
            try
            {
                var userInfo = GetLoginUser();
                HostelCreate createdhostel = await _accommodationService.CreateHostel(userInfo.Id, hostel);
                var objId = new ObjectId { Id = createdhostel.Id.ToString() };
                var responseObj = new ApiObjResponse<ObjectId>(message: hostelCreateSuccess, data: objId);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpPost]
        [Route("{hostelId}/rooms")]
        public async Task<IActionResult> SetRoomInfo(int hostelId, [FromBody] List<HostelRoomCreate> rooms)
        {
            try
            {
                var userLogin = GetLoginUser();
                HostelRoomCreateResponse createdRooms = await _accommodationService.AddHostelRooms(hostelId, rooms);
                var responseObj = new ApiObjResponse<HostelRoomCreateResponse>(message: hostelCreateSuccess, data: createdRooms);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }

        }

        [HttpPut]
        [Route("{roomId}/update")]
        public async Task<IActionResult> UpdateRoomInfo(int roomId, [FromBody] HostelRoomUpdate room)
        {
            try
            {
                var userLogin = GetLoginUser();
                await _accommodationService.EditHostelRoomInfo(roomId, room);
                var objId = new ObjectId { Id = roomId.ToString() };
                var responseObj = new ApiObjResponse<ObjectId>(message: hostelCreateSuccess, data: objId);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }

        }

        [HttpDelete]
        [Route("{hostelId}/remove")]
        public async Task<IActionResult> RemoveRooms(int hostelId, [FromBody]List<int> roomIds)
        {
            try
            {
                var userLogin = GetLoginUser();
                await _accommodationService.RemoveHostelRooms(hostelId, roomIds);
                var responseObj = new ApiObjResponse<ObjectId>(message: hostelCreateSuccess, data: null);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }

        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetHostelLists()
        {
            try
            {
                var userLogin = GetLoginUser();
                var hostellist = await _accommodationService.GetHostelList();
                var responseObj = new ApiListResponse<HostelGet>(message: hostelCreateSuccess, data: hostellist);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }

        }

        [HttpGet]
        [Route("rooms")]
        public async Task<IActionResult> GetHostelRoomLists(string eventId)
        {
            try
            {
                var userLogin = GetLoginUser();
                var roomList = new List<HostelRoomGet>();
                if(eventId != null)
                {
                    roomList = await _accommodationService.GetRoomForEventByHostel();
                }
                else
                {
                    roomList = await _accommodationService.GetRoomsForHostel();
                }
                var responseObj = new ApiListResponse<HostelRoomGet>(message: hostelCreateSuccess, data: roomList);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }

        }

        [HttpPost]
        [Route("space")]
        public async Task<IActionResult> AddSpaceToEventHostel([FromBody] HostelSpaceCreate hostel)
        {
            try
            {
                var userId = GetLoginUser();
                var spaceId = await _accommodationService.AddEventHostelSpace(hostel.EventId, hostel);
                var objId = new ObjectId { Id = spaceId };
                var response = new ApiObjResponse<ObjectId>(message: "", data: objId);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return HandleExceptionError(ex);
            }
           

        }

        [HttpPost]
        [Route("{eventId}/space/generate")]
        public async Task<IActionResult> GenerateSpaceFromHostels(string eventId, [FromBody] HostelIdListModel hostel)
        {
            try
            {
                var userId = GetLoginUser();
                var spaceId = await _accommodationService.GenerateEventSpaces(eventId, hostel.Ids);
                var response = new ApiObjResponse<ObjectId>(message: "", data: null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }


        }

        [HttpPost]
        [Route("space/update")]
        public async Task<IActionResult> UpdateEventSpaceHostel([FromBody] HostelSpaceUpdate hostel)
        {
            try
            {
                var userId = GetLoginUser();
                if(hostel.UpdateType == SpaceUpdateType.Space)
                {
                    await _accommodationService.UpdateEventSpaces(hostel.EntityIds, hostel);
                }
                else
                {
                    await _accommodationService.UpdateEventSpaceByRooms(hostel.EntityIds, hostel);
                }
                var response = new ApiObjResponse<ObjectId>(message: "", data: null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }


        }

        [HttpPost]
        [Route("space/remove")]
        public async Task<IActionResult> RemoveHostelEventSpace([FromBody] HostelSpaceUpdate hostel)
        {
            try
            {
                var userId = GetLoginUser();
                if (hostel.UpdateType == SpaceUpdateType.Space)
                {
                    await _accommodationService.RemoveEventHostelSpace(hostel.EntityIds);
                }
                else
                {
                    await _accommodationService.RemoveEventHostelSpaceByRoom(hostel.EntityIds);
                }
                var response = new ApiObjResponse<ObjectId>(message: "", data: null);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

    }
}
