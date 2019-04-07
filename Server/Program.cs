using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MQTTnet.AspNetCore;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                //Crea server usando Kestrel 
                .UseKestrel(o =>
                {
                    //Sente i pacchetti MQTT da ogni IP sulla porta 1883
                    o.ListenAnyIP(1883, l => l.UseMqtt());
                    //Sente i pacchetti HTTP su 5000
                    o.ListenAnyIP(5000);
                })
                .UseStartup<Startup>();
        }
    }
}