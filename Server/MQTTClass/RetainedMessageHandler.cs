using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Server;
using Newtonsoft.Json;

namespace Server.MQTTClass
{
    public class RetainedMessageHandler : IMqttServerStorage
    {
        private const string Filename = "D://MQTT/Test/RetainedMessage.json";

        public Task SaveRetainedMessagesAsync(IList<MqttApplicationMessage> messages)
        {
            File.WriteAllText(Filename, JsonConvert.SerializeObject(messages));
            return Task.FromResult(0);
        }

        public Task<IList<MqttApplicationMessage>> LoadRetainedMessagesAsync()
        {
            IList<MqttApplicationMessage> retainedMessages;
            if (File.Exists(Filename))
            {
                var json = File.ReadAllText(Filename);
                retainedMessages = JsonConvert.DeserializeObject<List<MqttApplicationMessage>>(json);
            }
            else
            {
                retainedMessages = new List<MqttApplicationMessage>();
            }

            return Task.FromResult(retainedMessages);
        }
    }
}