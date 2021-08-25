using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjetoEstudo.Dao;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace ProjetoEstudo.Providers.Health
{
	public class HealthCheckProvider
	{
		public IWebHostEnvironment Environment { get; private set; }

		public HealthCheckProvider(IWebHostEnvironment environment)
		{
			this.Environment = environment;
		}//func

		public static HealthCheckProvider GetInstance(IWebHostEnvironment environment) => new HealthCheckProvider(environment);

		public async Task WriteHealthStatusAsJson(HttpContext context, HealthReport report)
		{
			dynamic info = new ExpandoObject();
			info.Now = DateTime.Now;
			info.Environment = this.Environment.EnvironmentName;
			info.BaseConnection = report.Entries[nameof(BancoContext)].Status == HealthStatus.Healthy;

			JObject applicationInfo = JObject.FromObject(info);

			await context.Response.WriteAsync(JsonConvert.SerializeObject(applicationInfo));
		}//func

	}//class
}
