using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Accomodations
{
    public class HostelGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class HostelRoomGet
    {
        public int Id { get; set; }
        public string Tag { get; set;}
    }
}
