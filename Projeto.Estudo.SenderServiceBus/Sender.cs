using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using Projeto.Estudo.SenderServiceBus.Interfaces;
using ProjetoEstudo.Utils;
using System.Threading.Tasks;

namespace Projeto.Estudo.SenderServiceBus
{
	public class Sender : ISender
	{

		public Sender()
		{

		}

		public async Task SendMessageAsync<T>(T message, string queueOrTopicName)
		{
			string connectionString = UtilitiesConfig.GetAppSetting("AzureServiceBus");

			await using (ServiceBusClient cliente = new ServiceBusClient(connectionString))
			{
				ServiceBusSender serviceBusSender = cliente.CreateSender(queueOrTopicName);

				string jsonMessage = JsonConvert.SerializeObject(message);

				ServiceBusMessage serviceBusMessage = new ServiceBusMessage(jsonMessage);

				await serviceBusSender.SendMessageAsync(serviceBusMessage);
			}//using
		}//func

	}//class
}//namespace
