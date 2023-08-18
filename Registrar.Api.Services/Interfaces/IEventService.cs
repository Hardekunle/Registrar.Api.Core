using Registrar.Api.Services.Dto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventCreate> CreateEventDetails(string createdBy, EventCreate createEvent);
        Task<EventUpdate> UpdateEventDetails(string eventId, EventUpdate updateEvent);
        Task<string> DisableEvents(string eventId);
        Task<EventDetails> GetEventDetails(string eventId);
        Task<List<EventList>> GetEvents(EventListFilter filter);

    }
}
