using A2BBAPI.Data;
using A2BBCommon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace A2BBAPI
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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services available during DI.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<A2BBApiDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("A2BBApiConnection")));

            var authUserPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("sub")
                .Build();

            var authGranterPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireAssertion(h => h.User.Claims.FirstOrDefault(c => c.Type == "sub") == null)
                .Build();

            services.AddMemoryCache();
            services.AddMvc();

            services.AddAuthorization(options => options.AddPolicy("User", authUserPolicy));
            services.AddAuthorization(options => options.AddPolicy("Granter", authGranterPolicy));

            services.AddCors();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = Constants.IDENTITY_SERVER_ENDPOINT,
                AuthenticationScheme = "Bearer",
                AllowedScopes = { Constants.A2BB_API_RESOURCE_NAME },
                RequireHttpsMetadata = false
            });

            app.UseMvc();
        }
        #endregion
    }
}
