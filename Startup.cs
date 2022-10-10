using Accepted_Assignment.Entities;
using Accepted_Assignment.Repositories;
using Accepted_Assignment.Repositories.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Accepted_Assignment
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(options =>
				   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IMatchRepository, MatchRepository>();
			services.AddScoped<IMatchOddsRepository, MatchOddsRepository>();
			services.AddCors();

			services.AddSwaggerGen(options =>{
				var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
			});


			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseAuthorization();

			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
			});

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
				endpoints.MapControllerRoute(
						"default", "{controller=Account}/{action=Index}/{id?}")
					.RequireAuthorization();
			});
		}
	}
}
