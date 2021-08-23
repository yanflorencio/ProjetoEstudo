using System.Threading.Tasks;

namespace Projeto.Estudo.SenderServiceBus.Interfaces
{
	public interface ISender
	{
		public Task SendMessageAsync<T>(T message, string topicName);
	}//class
}//namespace
