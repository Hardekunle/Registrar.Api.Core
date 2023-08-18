using Registrar.Api.Data.Repository;
using Registrar.Api.Services.Dto.Events;
using Registrar.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Logic
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unit;
        public EventService(IUnitOfWork unit)
        {
            _unit= unit;
        }
        public async Task<EventCreate> CreateEventDetails(string createdBy, EventCreate createEvent)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DisableEvents(string eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<EventDetails> GetEventDetails(string eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EventList>> GetEvents(EventListFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<EventUpdate> UpdateEventDetails(string eventId, EventUpdate updateEvent)
        {
            throw new NotImplementedException();
        }
    }
}
