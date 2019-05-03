using System.Collections.Generic;

namespace tgsc_functions.Models
{
    [System.Serializable]
    public class EventResult
    {
        public int EventId { get; set; }
        public int EventPosition { get; set; }
        public int EventDays { get; set; }
        public int RoundNumber { get; set; }

        public EventResult()
        { }
    }
}