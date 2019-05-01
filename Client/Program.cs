using System;
using System.Threading.Tasks;
using Client.MQTT;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var name = "test";
            var serverIp = "localhost:1883";
            var topic = "swag";
            //var client = new Manager(name,serverIp);
            Console.ReadLine();
            var options = new ManagedMqttClientOptionsBuilder().
                WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(name)
                    //.WithCredentials("panon", "dellaMarra")
                    .WithTcpServer(serverIp)
                    //.WithTls()
                    .Build())
                .Build();
            var managedMqttClient = new MqttFactory().CreateManagedMqttClient();
            await managedMqttClient.StartAsync(options);
            
            if(managedMqttClient.IsStarted) Console.WriteLine("Client startato");
            if (managedMqttClient.IsConnected) Console.WriteLine("Client connesso");
            Console.ReadLine();
            //await managedMqttClient.SubscribeAsync(topic);
            //Console.ReadLine();
            await managedMqttClient.PublishAsync(new MqttApplicationMessageBuilder().WithTopic(topic)
                .WithPayload("Hello World")
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build()
            );
            Console.ReadLine();
            //if (await client.SubscribeTopic(topic)) Console.WriteLine("Subscribed to " + topic);
            //else Console.WriteLine("Cannot subscribe");
            //Console.ReadLine();
            //if (client.SendMessage("swag", "Hello")) Console.WriteLine("Message sent to " + topic);
            //else Console.WriteLine("Cannot send message"); 
            //if (client.SendMessage("swag", "I'm Smenz")) Console.WriteLine("Message sent to " + topic);
            //else Console.WriteLine("Cannot send message"); 
        }
    }
}
