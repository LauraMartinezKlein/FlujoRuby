
namespace API_Productor.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using ApiProductor;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        static string connectionString = "Endpoint=sb://queuelaura.servicebus.windows.net/;SharedAccessKeyName=Enviar;SharedAccessKey=iMEVk2veNB+LCG9dPMryaBA59rOVuaWZ4NxZ7ZXSy+Y=;EntityPath=cola1";
        static string queueName = "cola1";
        

        [HttpPost]
        public async Task<bool> enviarAsync([FromBody] Data data)
        {
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                string mensaje = JsonConvert.SerializeObject(data);

                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }
            return true;
        }
    }
}
