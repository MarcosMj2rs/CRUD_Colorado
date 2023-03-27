using Colorado.Core.Interfaces;
using Colorado.Infrastructure;
using Colorado.Infrastructure.Data;
using Colorado.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Colorado.Web.Api.RestFull
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
			//copie a partit daqui
			var cultureInfo = new CultureInfo("pt-BR");
			cultureInfo.NumberFormat.CurrencySymbol = "R$";
			CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
			CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

			string[] urls = "*,*".Split(",");

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.WithOrigins(urls)
									  .WithMethods("GET", "POST", "PUT")
									  .AllowAnyHeader()
									  .AllowAnyMethod()
									  .AllowCredentials());
			});

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Colorado.Web.Api.RestFull", Version = "v1" });
			});

			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(
			Configuration.GetConnectionString("DefaultConnection"),
			b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
			#region Repositories
			services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
			services.AddTransient<ICustomerRepositoryAsync, CustomerRepositoryAsync>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			#endregion
			services.AddHttpContextAccessor();
			services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var cultureInfo = new CultureInfo("pt-BR");
			cultureInfo.NumberFormat.CurrencySymbol = "R$";

			CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
			CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture(cultureInfo)
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Colorado.Web.Api.RestFull v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
