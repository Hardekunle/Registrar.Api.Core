using Registrar.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.Repository
{
    public interface IAccommodationRepo
    {
        Task<Hostel> AddAsync(Hostel accommodation);
        Task<HostelRoom> AddFloorAsync(HostelRoom floor);
        Task AddFloorListAsync(List<HostelRoom> floorInfos);
        Task AddListSpaceAsync(List<HostelSpace> hostelSpaces);
        Task AddRooms(List<HostelRoom> roomToBeCreated);
        Task AddSpaceAsync(HostelSpace spaceNew);
        Task<List<Hostel>> GetAll();
        Task<List<Hostel>> GetAllForEvent();
        Task<List<HostelRoom>> GetAllRooms();
        Task<List<HostelRoom>> GetAllRoomsForEvent();
        Task<List<HostelRoom>> GetAllRoomsWithHostelIds(List<int> hostelIds);
        Task<Hostel> GetById(int id);
        Task<HostelRoom> GetRoomById(object hostelId);
        Task<List<HostelRoom>> GetRoomsByIds(List<int> roomIds);
        Task<List<HostelSpace>> GetSpaceByIds(List<int> spaceId);
        Task<List<HostelSpace>> GetSpaceByRoomIds(List<int> roomIds);
        Task RemoveRoomsAsync(List<HostelRoom> roomsToRemove);
        Task RemoveSpacesAsync(List<HostelSpace> rooms);
    }
}
