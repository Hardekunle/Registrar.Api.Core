using Registrar.Api.Data.Entities;
using Registrar.Api.Data.Repository;
using Registrar.Api.Services.Dto.Accomodations;
using Registrar.Api.Services.Interfaces;
using Registrar.Api.Services.MapProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Logic
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IUnitOfWork _unit;
        private const string accommodationNotFound = "unable to retrieve accommodation";
        private const string roomNotFound = "unable to retrieve room deta";
        public AccommodationService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<HostelCreate> CreateHostel(string userId, HostelCreate hostel)
        {
            Hostel hostelToCreate = AccommodationMappings.Map(hostel, new Hostel());
            Hostel hostelCreated = await _unit.Hostels.AddAsync(hostelToCreate);
            hostel.Id = hostelCreated.Id;
            _unit.SaveToDatabase();
            return hostel;
        }

        public async Task<HostelRoomCreateResponse> AddHostelRooms(int hostelId, List<HostelRoomCreate> hostelRoomCreates)
        {
            Hostel hostelInfo = await _unit.Hostels.GetById(hostelId);
            if(hostelInfo == null)
            {
                throw new BadRequestException("unable to retrieve hostel details");
            }

            if(hostelInfo.MaxRoomNumbers == hostelInfo.CreatedRoomNumber)
            {
                throw new BadRequestException("maximum hostel room capacity exhausted");
            }

            var remainingRoomCount = hostelInfo.MaxRoomNumbers - hostelInfo.CreatedRoomNumber;
            var roomsToBeCreated = new List<HostelRoom>();

            var allocatedRooms = new List<HostelRoomCreate>();

            foreach(var item in hostelRoomCreates)
            {
                if(remainingRoomCount <= 0)
                {
                    break;
                }
                roomsToBeCreated.Add(AccommodationMappings.Map(item, new HostelRoom()));
                allocatedRooms.Add(item);
                
            }

            foreach(var item in allocatedRooms)
            {
                hostelRoomCreates.Remove(item);
            }

            hostelInfo.CreatedRoomNumber += roomsToBeCreated.Count();

            await _unit.Hostels.AddRooms(roomsToBeCreated);

            _unit.SaveToDatabase();

            return new HostelRoomCreateResponse
            {
                AllocatedRooms = allocatedRooms,
                NonAllocatedRooms = hostelRoomCreates
            };
        }

        public async Task RemoveHostelRooms(int hostelId, List<int> roomIds)
        {
            var hostelInfo = await _unit.Hostels.GetById(hostelId);

            if (hostelInfo == null)
            {
                throw new BadRequestException("");
            }

            List<HostelRoom> roomInfos = await _unit.Hostels.GetRoomsByIds(roomIds);
            await _unit.Hostels.RemoveRoomsAsync(roomInfos);

            hostelInfo.CreatedRoomNumber -= roomInfos.Count();
            _unit.SaveToDatabase();

        }

        public async Task EditHostelRoomInfo(int roomId, HostelRoomUpdate roomUpdate)
        {
            var existingRoom = await _unit.Hostels.GetRoomById(roomId);
            if(existingRoom == null)
            {
                throw new BadRequestException(roomNotFound);
            }

            existingRoom.MaxCapacity = roomUpdate.MaxCapacity;

            _unit.SaveToDatabase();

        }

        public async Task<List<string>> GenerateEventSpaces(string eventsId, List<int> hostelIds)
        {
            var arrChar = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };
            List<HostelRoom> roomInfos = await _unit.Hostels.GetAllRoomsWithHostelIds(hostelIds);
            List<HostelSpace> hostelSpaces = new List<HostelSpace>();
            foreach(var room in roomInfos)
            {
                for(int i =0; i< room.MaxCapacity; i++)
                {
                    var hostelSpace = new HostelSpace
                    {
                        RoomTag = room.Tag,
                        SpaceTag = arrChar[i],
                        EventId = eventsId,
                        HostelName = room.Hostel.Name
                    };
                    hostelSpaces.Add(hostelSpace);

                }
                await _unit.Hostels.AddListSpaceAsync(hostelSpaces);
            }

            _unit.SaveToDatabase();

            return new List<string>();
        }

        public async Task<string> AddEventHostelSpace(string eventId, HostelSpaceCreate space)
        {
            var room = await _unit.Hostels.GetRoomById(space.RoomId);
            if(room == null)
            {
                throw new BadRequestException(roomNotFound);
            }
            var spaceNew = new HostelSpace
            {
                EventId = eventId,
                RoomTag = room.Tag,
                HostelName = room.Hostel.Name,
                SpaceTag = space.SpaceTag,
            };

            await _unit.Hostels.AddSpaceAsync(spaceNew);

            _unit.SaveToDatabase();
            return "";
        }

        public async Task UpdateEventSpaceByRooms(List<int> roomIds, HostelSpaceUpdate space)
        {
            List<HostelSpace> spaces = await _unit.Hostels.GetSpaceByRoomIds(roomIds);

            foreach(var room in spaces)
            {
                room.SpaceType = space.SpaceType;
            }

            _unit.SaveToDatabase();
        }

        public async Task UpdateEventSpaces(List<int> spaceIds, HostelSpaceUpdate space)
        {
            List<HostelSpace> spaces = await _unit.Hostels.GetSpaceByIds(spaceIds);

            foreach (var room in spaces)
            {
                room.SpaceType = space.SpaceType;
            }

            _unit.SaveToDatabase();
        }

        public async Task RemoveEventHostelSpaceByRoom(List<int> roomIds)
        {
            List<HostelSpace> rooms = await _unit.Hostels.GetSpaceByRoomIds(roomIds);

            await _unit.Hostels.RemoveSpacesAsync(rooms);

            _unit.SaveToDatabase();
        }

        public async Task RemoveEventHostelSpace(List<int> spaceId)
        {
            List<HostelSpace> rooms = await _unit.Hostels.GetSpaceByIds(spaceId);

            await _unit.Hostels.RemoveSpacesAsync(rooms);

            _unit.SaveToDatabase();
        }

        public async Task<List<HostelGet>> GetHostelList()
        {
            List<Hostel> hostels = await _unit.Hostels.GetAll();
            var hostelsResp = new List<HostelGet>();
            foreach(var item in hostels)
            {
                hostelsResp.Add(AccommodationMappings.Map(item, new HostelGet()));
            }
            return hostelsResp;
        }

        public async Task<List<HostelRoomGet>> GetRoomsForHostel()
        {
            List<HostelRoom> hostels = await _unit.Hostels.GetAllRooms();
            var hostelsResp = new List<HostelRoomGet>();
            foreach (var item in hostels)
            {
                hostelsResp.Add(AccommodationMappings.Map(item, new HostelGet()));
            }
            return hostelsResp;
        }

        public async Task<List<HostelGet>> GetHostelListForEvent()
        {
            List<Hostel> hostels = await _unit.Hostels.GetAllForEvent();
            var hostelsResp = new List<HostelGet>();
            foreach (var item in hostels)
            {
                hostelsResp.Add(AccommodationMappings.Map(item, new HostelGet()));
            }
            return hostelsResp;
        }

        public async Task<List<HostelRoomGet>> GetRoomForEventByHostel()
        {
            List<HostelRoom> hostels = await _unit.Hostels.GetAllRoomsForEvent();
            var hostelsResp = new List<HostelRoomGet>();
            foreach (var item in hostels)
            {
                hostelsResp.Add(AccommodationMappings.Map(item, new HostelGet()));
            }
            return hostelsResp;
        }
    }
}
