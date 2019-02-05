using Audatex.B2B.SDK.FNOL.Entities;
using Audatex.B2B.SDK.FNOL.Mongo;
using Audatex.B2B.SDK.FNOL.RabbitMQ;
using Audatex.B2B.SDK.FNOL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using Rebus.RabbitMq;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.InMem;
using Steeltoe.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using System;
using Rebus.Config;
using System.Collections.Generic;

namespace Audatex.B2B.SDK.FNOL
{
	public class Startup
    {
		private readonly IHostingEnvironment _hostingEnvironment;
		public IConfiguration Configuration { get; }

		public Startup(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;

			Configuration = new ConfigurationBuilder()
				.SetBasePath(hostingEnvironment.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
				.AddCloudFoundry()
				.AddEnvironmentVariables()
				.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Audatex B2B SDK for FNOL", Version = "v1" });
			});

			var mongoConnectionString = Configuration["vcap:services:mongodb-dev:0:credentials:uri"];

			//Repositories
			services.AddTransient<IRepository<AssignmentEntity>>(provider => new MongoRepository<AssignmentEntity>(mongoConnectionString));
			
			//Services
			services.AddScoped<IAssignmentService, AssignmentService>();

            // Rebus
            // Register handlers 
            //services.AutoRegisterHandlersFromAssemblyOf<Handler1>();
            var rabbitmqConnectionString = Configuration["vcap:services:rabbitmq-dev:0:credentials:uri"];
			//const string queueName = "Blah";
			// Configure and register Rebus
			services.AddRebus(configure => configure
				.Options(o => o.LogPipeline(verbose: true))
				.Logging(l => l.Console())
				 //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "Messages"))
				 //.Subscriptions(s => s.StoreInMemory())
				 .Transport(t => t.UseRabbitMqAsOneWayClient(new List<ConnectionEndpoint>() { new ConnectionEndpoint() { ConnectionString = rabbitmqConnectionString } }))
				 //.Transport(t => t.UseRabbitMq( connectionString, queueName))
				 
				//.Subscriptions(s => s.StoreInMemory())
				//.Routing(r => r.TypeBased().MapAssemblyOf<AssignmentMessage>("Recall"))
				);
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audatex B2B SDK for FNOL V1");
			});

        }
    }
}
