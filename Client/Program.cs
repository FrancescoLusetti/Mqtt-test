using Client.MQTT;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = "test";
            var serverIp = "127.0.0.1:5000";
            var client = new Manager(name,serverIp);
        }
    }
}
