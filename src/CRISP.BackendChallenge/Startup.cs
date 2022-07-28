using CRISP.Backend.Challenge.Context;
using CRISP.Backend.Challenge.Context.Models;
using CRISP.BackendChallenge.Context;
using CRISP.BackendChallenge.Helpers;
using CRISP.BackendChallenge.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CRISP.Backend.Challenge;

public class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		// Add services to the container.
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.AddSwaggerGen(c => { c.SchemaFilter<EnumSchemaFilter>(); });
		services.AddEntityFrameworkSqlite().AddDbContext<ApplicationDbContext>();
		services.AddTransient<IRepository, ContextRepository>();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseRouting();
		app.UseHttpsRedirection();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action=Index}/{id?}");
		});

		using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
		var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		context?.Database.Migrate();
	}
}