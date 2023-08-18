using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Accomodations
{
    public enum HostelGender
    {
        Male,
        Female
    }
     
    public class HostelIdListModel
    {
        public List<int> Ids { get; set; }
    }
    public class HostelSpaceCreate
    {
        public string EventId { get; set; }   
        public int RoomId { get; set; }
        public string SpaceTag { get; set; }
    }
    public class HostelRoomCreate
    {
        public int HostelId { get; set; }
        public int Floor { get; set; }
        public string WingTag { get; set; }
        public string RoomNumber { get; set; }
        public int RoomCapacity { get; set; }

    }

    public class HostelRoomCreateResponse
    {
        public List<HostelRoomCreate> AllocatedRooms { get; set; }
        public List<HostelRoomCreate> NonAllocatedRooms { get; set; }
    }

    public class HostelCreate
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MaxRoomCapacity { get; set; }
        public HostelGender GenderAdmissible { get; set; }

    }
}
