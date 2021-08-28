using Projeto.Estudo.SenderServiceBus.Interfaces;
using ProjetoEstudo.Utils;
using Rebus.Activation;
using Rebus.Bus.Advanced;
using Rebus.Config;
using Rebus.Topic;
using System;

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

			ISyncBus bus = this.GetConnectionForPublish(connectionString, topicName);

			try
			{
				bus.Publish(message);

				return true;
			}//try
			catch (Exception e)
			{
				//LOGA ERRO
				return false;
			}//catch

		}//func


		public ISyncBus GetConnectionForPublish(string connectionString, string topicName)
		{
			string defaultQueue = "default";

			using (BuiltinHandlerActivator activator = new BuiltinHandlerActivator())
			{

				Configure
						.With(activator)
						.Transport(t => t.UseAzureServiceBus(connectionString, defaultQueue))
						.Options(o => o.Decorate<ITopicNameConvention>(bean => new TopicNameResolver(topicName)))
						.Start();

				ISyncBus bus = activator.Bus.Advanced.SyncBus;

				return bus;
			}
		}//func

	}//class
}//namespace
