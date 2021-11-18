namespace ConnectSDK.UnitTests.VoiceApi.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Voice.Models.V2.Calls;

    public class CallEventHandler
    {
        private readonly List<CallEvent> events;

        public CallEventHandler()
        {
            this.events = new List<CallEvent>();
        }

        public List<CallEvent> ProcessedEvents => this.events.ToList();

        public void Process(CallEvent @event)
        {
            this.events.Add(@event);
        }
    }
}
