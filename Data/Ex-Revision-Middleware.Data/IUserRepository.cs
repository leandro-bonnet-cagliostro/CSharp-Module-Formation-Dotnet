using Ex_Revision_Middleware.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Data
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<Role>> GetRoleByUserIdAsync(int userId);
    }
}
