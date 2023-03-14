using System.Collections.Generic;
using System.Linq;

namespace MyFeature.Feature.MyFeature
{
    public class EventService
    {
        private readonly List<Events> _events;

        public EventService()
        {
            _events = new List<Events> { new Events() };
        }

        public void AddEvent(Events newEvent)
        {
            _events.Add(newEvent);
        }

        public void UpdateEvent(Guid eventId, Events updatedEvent)
        {
            var existingEvent = _events.FirstOrDefault(e => e.idEvent == eventId);
            if (existingEvent != null)
            {
                existingEvent.Name = updatedEvent.Name;
                existingEvent.Description = updatedEvent.Description;
                existingEvent.BeginTime = updatedEvent.BeginTime;
                existingEvent.EndTime = updatedEvent.EndTime;
                existingEvent.Imgid = updatedEvent.Imgid;
                existingEvent.Spaceid = updatedEvent.Spaceid;
            }
        }

        public Events GetEvent(Guid eventId)
        {
            return _events.FirstOrDefault(e => e.idEvent == eventId);
        }

        public IEnumerable<Events> GetAllEvents()
        {
            return _events.ToList();
        }

        public void DeleteEvent(Guid eventId)
        {
            var existingEvent = _events.FirstOrDefault(e => e.idEvent == eventId);
            if (existingEvent != null)
            {
                _events.Remove(existingEvent);
            }
        }
    }
}
