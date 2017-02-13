using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using A2BBCommon.Models;
using A2BBIdentityServer.Models;

namespace A2BBIdentityServer.Data
{
    /// <summary>
    /// The Identity server DB context.
    /// </summary>
    public class A2BBIdSrvDbContext : IdentityDbContext<User>
    {
        #region Public methods
        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        /// <param name="options">The DB context builder options.</param>
        public A2BBIdSrvDbContext(DbContextOptions<A2BBIdSrvDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Called before model creation.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        #endregion
    }
}
