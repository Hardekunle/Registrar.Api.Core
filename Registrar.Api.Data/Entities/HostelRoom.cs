using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.Entities
{
    public enum RoomStatus
    {
        inactive,
        active
    }
    public class HostelRoom
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public string WingLetter { get; set; }
        public int MaxCapacity { get; set; }
        public int NoOccupied { get; set; }
        public int HostelId { get; set; }
        public RoomStatus Status { get; set; }
        public string RoomNumber { get; set; }
        public string Tag { get; set; }
        public Hostel Hostel { get; set; }
    }
}
