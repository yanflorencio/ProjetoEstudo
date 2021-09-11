namespace Projeto.Estudo.SenderServiceBus.Interfaces
{
	public interface ISender
	{
		public void SendMessage(object message, string topicName);
	}//class
}//namespace
