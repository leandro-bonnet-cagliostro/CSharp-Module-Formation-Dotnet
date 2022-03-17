using Ex_Revision_Middleware.Data.Providers.Sql.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ex_Revision_Middleware.Data.Providers.Sql
{
    public class RevisionEtMiddlewareDbContext : DbContext // versin d'avant
    //public class RevisionEtMiddlewareDbContext : ApiAuthorizationDbContext<User>
    {
        public RevisionEtMiddlewareDbContext(DbContextOptions options) : base(options)
        {

        }
        /*public RevisionEtMiddlewareDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalOptions
            ) : base(options,operationalOptions)
        {

        }*/

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
