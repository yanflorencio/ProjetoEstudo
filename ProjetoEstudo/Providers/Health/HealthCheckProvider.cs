using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
			//using BancoContext modelContext = this.BancoContext;

			dynamic info = new ExpandoObject();
			info.Now = DateTime.Now;
			info.Environment = this.Environment.EnvironmentName;
			//info.BaseConnection = TestDbConnection(modelContext);

			JObject applicationInfo = JObject.FromObject(info);

			context.Response.ContentType = "application/json";

			await context.Response.WriteAsync(JsonConvert.SerializeObject(applicationInfo));
		}//func

		private static bool TestDbConnection(BancoContext context)
		{
			try
			{
				context.Database.ExecuteSqlRaw("SELECT 1");
				return true;
			}//try
			catch (Exception e)
			{
				return false;
			}//catch
		}//func
	}//class
}
