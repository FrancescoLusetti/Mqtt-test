using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.MQTT
{
    class Manager
    {

        private IManagedMqttClient managedMqttClient;
        private IManagedMqttClientOptions clientOptions;

        public Manager(string name, string ip)
        {
            var options = new ManagedMqttClientOptionsBuilder().
                WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(name)
                    //.WithCredentials("panon", "dellaMarra")
                    .WithTcpServer(ip)
                    //.WithTls()
                    .Build())
                .Build();

            var _managedMqttClient = new MqttFactory().CreateManagedMqttClient();
            managedMqttClient = _managedMqttClient;
            clientOptions = options;
        }

        public async Task StartManager()
        {
            await managedMqttClient.StartAsync(clientOptions);
        }

        public async Task<bool> SubscribeTopic(string topic)
        {
            if (!managedMqttClient.IsStarted || !managedMqttClient.IsConnected) return false;
            await managedMqttClient.SubscribeAsync(topic);
            return true;

        }

        public bool SendMessage(string topic, string message)
        {
            var mqttMessage = CreateMessage(topic, message);
            if (managedMqttClient.IsStarted && managedMqttClient.IsConnected)
            {
                managedMqttClient.PublishAsync(mqttMessage);
                //managedMqttClient.ApplicationMessageProcessed;
                return true;
            }
            return false;
        }

        //public List<string> SubscribedTopic()
        //{
        //    managedMqttClient.
        //}
         
        private MqttApplicationMessage CreateMessage(string topic, string message)
        {
            return new MqttApplicationMessageBuilder().
                WithTopic(topic).
                WithPayload(message).
                WithExactlyOnceQoS().
                WithRetainFlag().
                Build();
        }

    }
}
