using Registrar.Api.Services.Dto.Accomodations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Interfaces
{
    public interface IAccommodationService
    {
        Task<HostelRoomCreateResponse> AddHostelRooms(int hostelId, List<HostelRoomCreate> hostelRoomCreates);
        Task EditHostelRoomInfo(int roomId, HostelRoomUpdate roomUpdate);
        Task RemoveHostelRooms(int hostelId, List<int> roomIds);
        Task<List<HostelRoomGet>> GetRoomsForHostel();
        Task<List<HostelGet>> GetHostelList();
        Task<List<HostelGet>> GetHostelListForEvent();
        Task<List<HostelRoomGet>> GetRoomForEventByHostel();
        Task<string> AddEventHostelSpace(string eventId, HostelSpaceCreate space);
        Task UpdateEventSpaces(List<int> spaceIds, HostelSpaceUpdate space);
        Task UpdateEventSpaceByRooms(List<int> roomIds, HostelSpaceUpdate space);
        Task RemoveEventHostelSpaceByRoom(List<int> roomIds);
        Task RemoveEventHostelSpace(List<int> spaceId);
        Task<List<string>> GenerateEventSpaces(string eventsId, List<int> hostelIds);
        Task<HostelCreate> CreateHostel(string userId, HostelCreate hostel);
    }
}
