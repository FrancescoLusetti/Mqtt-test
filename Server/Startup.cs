using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet.AspNetCore;
using MQTTnet.Server;
using Server.MQTTClass;

namespace Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Opzioni del server MQTT
            var option = new MqttServerOptionsBuilder()

                .WithDefaultEndpointPort(1883)//Dove mandare i messaggi
                .WithEncryptedEndpointPort(1883) //Dove mandare i messaggi con criptazione TLS 1.2
                .WithEncryptionSslProtocol(System.Security.Authentication.SslProtocols.Tls12)
                .WithStorage(new RetainedMessageHandler()) //salva i messaggi in un file JSON
                //.WithApplicationMessageInterceptor() Mi serve per intercettare dei pacchetti
                //.WithSubscriptionInterceptor() serve ad intercettare delle scuiaocvbnsivottoscrizioni a dei determinati canali
                //.WithConnectionValidator() serve a mettere una password alla connessione
                //.WithDefaultCommunicationTimeout() serve a cambiare il tempo di timeout
                .WithConnectionBacklog(100)//quante persone si possono collegare contemporaneamente
                //.WithPersistentSessions()//non so cosa faccia ma credo sia utile
                .Build();


            //Crea il seever MQTT
            services.AddHostedMqttServer(option);

            //Gestisce le connessioni
            services.AddMqttConnectionHandler();
            //gestisce le connessioni in WebSocket
            services.AddMqttWebSocketServerAdapter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.Run(async context => { await context.Response.WriteAsync("Hello World!"); });

            //permette di usare il WWWROOT folder
            //app.UseStaticFiles();
        }
    }
}