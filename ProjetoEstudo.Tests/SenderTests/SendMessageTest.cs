using Moq;
using Projeto.Estudo.SenderServiceBus;
using Rebus.Activation;
using Rebus.Bus.Advanced;
using System.Collections.Generic;
using Xunit;

namespace ProjetoEstudo.Tests.SenderTests
{
	public class SendMessageTest
	{
		[Fact]
		public void TrySendMessageForSeviceBus_DoNoThrowException()
		{
			object messageSender = null;
			object messageExpected = null;

			Mock<Sender> mock = new Mock<Sender>();

			Mock<ISyncBus> mock1 = new Mock<ISyncBus>();
			mock1.Setup(m => m.Publish(It.IsAny<object>(), It.IsAny<IDictionary<string, string>>())).Callback<object, IDictionary<string, string>>(
				(message, dictionary) =>
				{
					messageSender = message;
				}
				);

			mock.Setup(m => m.GetConnectionForPublish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<BuiltinHandlerActivator>())).Returns(mock1.Object);


			Sender sender = mock.Object;

			sender.SendMessage(messageExpected, "teste");

			Assert.True(true);
		}
	}
}
