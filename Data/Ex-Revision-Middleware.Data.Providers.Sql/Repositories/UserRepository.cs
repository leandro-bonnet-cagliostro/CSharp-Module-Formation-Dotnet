using AutoMapper;
using Ex_Revision_Middleware.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Data.Providers.Sql.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RevisionEtMiddlewareDbContext context;
        private readonly IMapper mapper;

        public UserRepository(RevisionEtMiddlewareDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<User> CreateAsync(User user)
        {
            // this.context.RoleClaims
            var userDb = this.mapper.Map<Sql.Models.User>(user);
            this.context.Users.Add(userDb);
            await this.context.SaveChangesAsync();
            user.Id = userDb.Id;
            return user;
            // mapper l'objet core en objet sql
            // this.context.Courses.Add();
            // sauvegarder
            // retourner l'objet avec son id
        }

        public async Task<IEnumerable<Role>> GetRoleByUserIdAsync(int userId)
        {
            //var roleDb = this.context.Users.FirstOrDefaultAsync(u => u.Id == userId)
            // SELECT r.Id, r.Name From Role r 
            // INNER JOIN[dbo].[User] u ON u.RoleId = r.Id
            // WHERE u.Id = 5
            /*var roleQuery = from user in this.context.Users
                         join role in this.context.Roles on user.RoleId equals role.Id
                         where user.Id == userId
                         select role;*/

            // SELECT r.Id, r.Name FROM Role r
            // INNER JOIN [dbo].[UserRole] ur ON r.Id = ur.RoleId
            // Where ur.UserId = 8
            /*var roleQuery = from ur in this.context.UserRoles
                            join role in this.context.Roles on ur.UserId
                            where userId == ur.UserId
                            select role;*/

            //var roledb = await roleQuery.FirstOrDefaultAsync();
            //var roledb = await roleQuery.ToListAsync();
            var roleQuery = this.context.UserRoles.AsNoTracking().Where(ur => ur.UserId == userId).Select(ur => ur.Role);
            var roledb = await roleQuery.ToListAsync();
            return this.mapper.Map<IEnumerable<Role>>(roledb);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var userDb = await this.context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            return this.mapper.Map<User>(userDb);
        }
    }
}
