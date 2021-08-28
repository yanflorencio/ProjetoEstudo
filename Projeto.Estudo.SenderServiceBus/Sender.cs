using Projeto.Estudo.SenderServiceBus.Interfaces;
using ProjetoEstudo.Utils;
using Rebus.Activation;
using Rebus.Bus.Advanced;
using Rebus.Config;
using Rebus.Topic;
using System;
using System.Text.Json;

namespace Projeto.Estudo.SenderServiceBus
{
	public class Sender : ISender
	{
		private string ConnectioString { get; set; }

		public Sender()
		{
			this.ConnectioString = UtilitiesConfig.GetAppSetting("AzureServiceBus");
		}

		public bool SendMessage(object message, string topicName)
		{
			string connectionString = this.ConnectioString;

			using (BuiltinHandlerActivator activator = new BuiltinHandlerActivator())
			{
				try
				{
					ISyncBus bus = this.GetConnectionForPublish(connectionString, topicName, activator);

					string json = JsonSerializer.Serialize(message);

					bus.Publish(message);

					return true;
				}//try
				catch (Exception e)
				{
					//LOGA ERRO
					return false;
				}//catch
			}
		}//func

		public ISyncBus GetConnectionForPublish(string connectionString, string topicName, BuiltinHandlerActivator activator)
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

	}//class
}//namespace
