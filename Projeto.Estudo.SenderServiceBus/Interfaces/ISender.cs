namespace Projeto.Estudo.SenderServiceBus.Interfaces
{
	public interface ISender
	{
		public bool SendMessage(object message, string topicName);
	}//class
}//namespace
