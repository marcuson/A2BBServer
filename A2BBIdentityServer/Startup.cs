using A2BBCommon;
using A2BBCommon.Models;
using A2BBIdentityServer.Data;
using A2BBIdentityServer.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace A2BBIdentityServer
{
    /// <summary>
    /// Class used to bootstrap the applicaiton.
    /// </summary>
    public class Startup
    {
        #region Public properties
        /// <summary>
        /// The chosen configuration.
        /// </summary>
        public IConfigurationRoot Configuration { get; }
        #endregion

        #region Public methods
        /// <summary>
        /// Main method to bootstrap the applicaiton.
        /// </summary>
        /// <param name="env">The hosting environment.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services available during DI.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<A2BBIdSrvDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("A2BBIdSrvConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<A2BBIdSrvDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddCors();

            // Add application services.
            services.AddTransient<IProfileService, AspNetCoreIdentityProfileService>();

            // NOTE: In production, there are more flexible implementations than in-memory providers!
            // IMPORTANT: In production, do not use temporary signing but a valid certificate!
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<User>()
                .AddProfileService<AspNetCoreIdentityProfileService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseIdentity();
            
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = Constants.IDENTITY_SERVER_ENDPOINT,
                AuthenticationScheme = "Bearer",
                AllowedScopes = { Config.IDSRV_API_RESOURCE_NAME },
                RequireHttpsMetadata = false
            });

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseIdentityServer();
            app.UseMvc();
        }
        #endregion
    }
}
