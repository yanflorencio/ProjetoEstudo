using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projeto.Estudo.SenderServiceBus;
using Projeto.Estudo.SenderServiceBus.Interfaces;
using ProjetoEstudo.Dao;
using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Filtros;
using ProjetoEstudo.Providers.Health;
using ProjetoEstudo.Service;
using ProjetoEstudo.Service.Interfaces;
using ProjetoEstudo.Utils;

namespace ProjetoEstudo
{
	public class Startup
	{
		public IConfiguration Configuration { get; set; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			string connectionStrings = UtilitiesConfig.GetAppSetting("ProjetoEstudo");

			services.AddDbContext<BancoContext>(db => {
				db.UseSqlServer(connectionStrings);
			});

			services.AddMvc(options =>
			{
				options.Filters.Add(typeof(ErrorResponseFilter));
			}).AddXmlSerializerFormatters();

			services.AddHealthChecks();

			//DAO
			services.AddTransient<IJogoDao, JogoDao>();
			services.AddTransient<IClienteDao, ClienteDao>();
			services.AddTransient<IAlugadoDao, AlugadoDao>();

			//SERVICE
			services.AddTransient<IJogoService, JogoService>();

			//SERVICE_BUS
			services.AddTransient<ISender, Sender>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello World!");
				});

				endpoints.MapHealthChecks("/health", new HealthCheckOptions()
				{
					ResponseWriter = HealthCheckProvider.GetInstance(env).WriteHealthStatusAsJson
				});
			});
		}
	}
}
