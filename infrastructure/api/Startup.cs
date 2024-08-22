using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.app.Services;
using infrastructure.data.Repositories;
using core.domain.Interfaces.Repositories;
using core.domain.Interfaces;
using core.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.API {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {

			services.AddControllers();
			// services.AddScoped<UserService>()
			
			services.AddDbContext<SocialNetworkContext>(options =>
        	options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
        	new MySqlServerVersion(new Version(8, 0, 21)),
        	mySqlOptions => mySqlOptions.EnableRetryOnFailure()));

			services.AddScoped<PostRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<FollowerRepository>();

            // Registrar servicios
            services.AddScoped<FollowerService>();
            services.AddScoped<PostService>();
            services.AddScoped<UserService>();

			services.AddScoped<InterfacePostRepository<Post, string>, PostRepository>();
			services.AddScoped<InterfaceUserRepository, UserRepository>();
			services.AddScoped<FollowerInterface<Follower>, FollowerRepository>();

		}

		

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
