using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.Entities
{
    public enum SpaceStatus
    {
        Available,
        Occupied
    }
    public enum SpaceType
    {
        Regular,
        Special
    }

    public class HostelSpace
    {
        public int Id { get; set; }
        public string RoomTag { get; set; }
        public string SpaceTag { get; set; }
        public string HostelName { get; set; }
        public string EventId { get; set; }
        public string UserId { get; set; }
        public SpaceStatus SpaceStatus { get; set; }
        public SpaceType SpaceType { get; set; }
    }
}
