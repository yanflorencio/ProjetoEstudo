using Projeto.Estudo.SenderServiceBus.Interfaces;
using ProjetoEstudo.Utils;
using Rebus.Activation;
using Rebus.Bus.Advanced;
using Rebus.Config;
using Rebus.Topic;
using System.Text.Json;
using System.Transactions;

namespace Projeto.Estudo.SenderServiceBus
{
	public class Sender : ISender
	{
		private string ConnectioString { get; set; }

		public Sender()
		{
			this.ConnectioString = UtilitiesConfig.GetAppSetting("AzureServiceBus");
		}

		public void SendMessage(object message, string topicName)
		{
			using (var tx = new TransactionScope(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled))
			{
				string connectionString = this.ConnectioString;

				using (BuiltinHandlerActivator activator = new BuiltinHandlerActivator())
				{
					ISyncBus bus = this.GetConnectionForPublish(connectionString, topicName, activator);

					string json = JsonSerializer.Serialize(message);

					FileMessage clienteManagerClienteMessage = new FileMessage();
					clienteManagerClienteMessage.Message = json;

					bus.Publish(clienteManagerClienteMessage);
				}
				tx.Complete();
			}
		}//func

		public virtual ISyncBus GetConnectionForPublish(string connectionString, string topicName, BuiltinHandlerActivator activator)
		{
			string defaultQueue = "default";

				Configure
						.With(activator)
						.Transport(t => t.UseAzureServiceBus(connectionString, defaultQueue))
						.Options(o => o.Decorate<ITopicNameConvention>(bean => new TopicNameResolver(topicName)))
						.Start();

				ISyncBus bus = activator.Bus.Advanced.SyncBus;

				return bus;
		}//func

		public class FileMessage
		{
			public string Message { get; set; }
		}

	}//class
}//namespace
