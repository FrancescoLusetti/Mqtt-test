using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.MQTT
{
    class Manager
    {

        private IManagedMqttClient managedMqttClient;

        public Manager(string name, string ip)
        {
            var options = new ManagedMqttClientOptionsBuilder().
                WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(name)
                    //.WithCredentials("panon", "dellaMarra")
                    .WithTcpServer(ip)
                    .WithTls()
                    .Build())
                .Build();

            var _managedMqttClient = new MqttFactory().CreateManagedMqttClient();
            _managedMqttClient.StartAsync(options);
            managedMqttClient = _managedMqttClient;
        }

        public Boolean SendMessage(MqttApplicationMessage message)
        {
            if (managedMqttClient.IsStarted)
            {
                managedMqttClient.PublishAsync(message);
                //managedMqttClient.ApplicationMessageProcessed;
            }
            return false;
        }

        public MqttApplicationMessage CreateMessage(string topic, string message)
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
