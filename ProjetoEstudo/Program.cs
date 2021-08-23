using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProjetoEstudo.Utils;

namespace ProjetoEstudo
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging(loggin => {

					IConfigurationRoot config = new ConfigurationBuilder()
					.AddJsonFile(path: UtilitiesConfig.DEFAULT_CONFIG_FILE)
					.Build();

				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
