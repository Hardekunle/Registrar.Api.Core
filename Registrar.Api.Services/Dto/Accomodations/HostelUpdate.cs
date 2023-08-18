using Registrar.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Accomodations
{
    public class HostelUpdate
    {
    }

    public enum SpaceUpdateType
    {
        Space,
        Room
    }

    public class HostelSpaceUpdate
    {
        public SpaceUpdateType UpdateType { get; set; } 
        public SpaceType SpaceType { get; set;}
        public List<int> EntityIds { get; set; }
    }

    public class HostelRoomUpdate
    {
        public int MaxCapacity { get; set; }
    }
}
