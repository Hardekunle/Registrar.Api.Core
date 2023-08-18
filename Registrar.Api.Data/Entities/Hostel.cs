using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.Entities
{
    public class Hostel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int MaxRoomNumbers { get; set; }
        public int CreatedRoomNumber { get; set; }
    }
}
