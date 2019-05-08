using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Implementations;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConection")));
			services.AddTransient<WebStoreContextInitializer>();

			services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
			//services.AddSingleton<IProductData, InMemoryProductData>();
			services.AddScoped<IProductData, SqlProductData>();
			services.AddScoped<ICartService, CookieCartService>();
			services.AddScoped<IOrderService, SqlOrdersService>();

			services.AddIdentity<User, IdentityRole>(options =>
				{
					// тут можно сконфигурировать cookies
				})
				.AddEntityFrameworkStores<WebStoreContext>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(config =>
			{
				config.Password.RequiredLength = 3;
				config.Password.RequireDigit = false;
				config.Password.RequireLowercase = false;
				config.Password.RequireUppercase = false;
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequiredUniqueChars = 3;

				config.Lockout.MaxFailedAccessAttempts = 10;
				config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
				config.Lockout.AllowedForNewUsers = true;
			});

			services.ConfigureApplicationCookie(config =>
			{
				config.Cookie.HttpOnly = true;
				config.Cookie.Expiration = TimeSpan.FromDays(100);
				config.Cookie.MaxAge = TimeSpan.FromDays(100);

				config.LoginPath = "/Account/Login";
				config.LogoutPath = "/Account/Logout";
				config.AccessDeniedPath = "/Account/AccessDenied";

				config.SlidingExpiration = true;
			});

			services.AddMvc();

			services.AddAutoMapper(options =>
			{
				options.CreateMap<Employee, Employee>();
			});

			/* или можно так
			AutoMapper.Mapper.Initialize(options =>
			{
				options.CreateMap<Employee, Employee>();
			});
			*/
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, WebStoreContextInitializer db)
		{
			db.InitializeAsync().Wait();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				//app.UseBrowserLink();
			}

			app.UseStaticFiles();
			app.UseDefaultFiles();

			app.UseAuthentication();

			app.UseMvc(route => 
			{
				route.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

				route.MapRoute(
					name:"default",
					template:"{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
