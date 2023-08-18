using Registrar.Api.Data.Entities;
using Registrar.Api.Services.Dto.Accomodations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.MapProfiles
{
    public class AccommodationMappings
    {
        public static Hostel Map(HostelCreate createHostel, Hostel hostel)
        {
            hostel.Name = createHostel.Name;
            hostel.MaxRoomNumbers = createHostel.MaxRoomCapacity;
            return hostel;

        }

        public static HostelRoom Map(HostelRoomCreate item, HostelRoom hostelRoom)
        {
            var tagGen = new StringBuilder();
            hostelRoom.MaxCapacity = item.RoomCapacity;
            hostelRoom.WingLetter = item.WingTag;
            tagGen.Append(item.WingTag);
            hostelRoom.FloorNumber = item.Floor;
            tagGen.Append(item.Floor);
            hostelRoom.RoomNumber = item.RoomNumber;
            tagGen.Append(item.RoomNumber);
            hostelRoom.Tag = tagGen.ToString();

            return hostelRoom;
        }

        internal static HostelGet Map(Hostel item, HostelGet hostelGet)
        {
            throw new NotImplementedException();
        }

        internal static HostelRoomGet Map(HostelRoom item, HostelGet hostelGet)
        {
            throw new NotImplementedException();
        }
    }
}
