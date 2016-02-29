using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace openhabUWP.Remote.Models
{
    //{"topic":"smarthome/items/DemoSwitch/state","payload":"{\"type\":\"OnOffType\",\"value\":\"ON\"}","type":"ItemStateEvent"}
    //{"topic":"smarthome/items/DemoSwitch/statechanged","payload":"{\"type\":\"OnOffType\",\"value\":\"ON\",\"oldType\":\"OnOffType\",\"oldValue\":\"OFF\"}","type":"ItemStateChangedEvent"}

    public class EventData
    {
        public string Topic { get; set; }
        public EventPayload Payload { get; set; }
        public string Type { get; set; }

        public EventData()
        {
            Payload = new EventPayload();
        }

        public EventData(string topic, EventPayload payload, string type) : this()
        {
            Topic = topic;
            Payload = payload;
            Type = type;
        }
    }

    public class EventPayload
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public string OldType { get; set; }
        public string OldValue { get; set; }

        public EventPayload()
        {

        }

        public EventPayload(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public EventPayload(string type, string value, string oldType, string oldValue) : this(type, value)
        {
            this.OldType = oldType;
            this.OldValue = oldValue;
        }
    }

    public static class EventDataFluent
    {
        public static EventData ToEventData(this string input)
        {
            JsonObject jo;
            if (JsonObject.TryParse(input, out jo))
            {
                return jo.ToEventData();
            }
            return null;
        }

        public static EventData ToEventData(this JsonObject jo)
        {
            var topic = jo.GetNamedString("topic");
            var payload = jo.GetNamedString("payload");
            var type = jo.GetNamedString("type");

            return new EventData(topic, payload.ToEventPayload(), type);
        }

        public static EventPayload ToEventPayload(this string input)
        {
            JsonObject jo;
            if (JsonObject.TryParse(input, out jo))
            {
                return jo.ToEventPayload();
            }
            return null;
        }

        public static EventPayload ToEventPayload(this JsonObject jo)
        {
            var type = jo.GetNamedString("type", "");
            var value = jo.GetNamedString("value", "");
            var oldType = jo.GetNamedString("oldType", "");
            var oldValue = jo.GetNamedString("oldValue", "");

            return new EventPayload(type, value, oldType, oldValue);
        }
    }
}
