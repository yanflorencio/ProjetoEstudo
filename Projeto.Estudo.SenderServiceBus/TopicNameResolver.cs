using Rebus.Topic;
using System;

namespace Projeto.Estudo.SenderServiceBus
{
	public class TopicNameResolver : ITopicNameConvention
	{
		private string TopicName { get; set; }

		public TopicNameResolver(string topicName)
		{
			this.TopicName = topicName;
		}

		public string GetTopic(Type eventType)
		{
			return this.TopicName;
		}
	}//class
}//namespace
