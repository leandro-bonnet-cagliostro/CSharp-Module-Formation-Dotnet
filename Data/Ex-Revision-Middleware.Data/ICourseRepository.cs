using Ex_Revision_Middleware.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Data
{
    public interface ICourseRepository
    {
        Task<Course> CreateAsync(Course course);

        Task<IEnumerable<Course>> GetAllAsync(CourseSearchParameters searchParameters);
        
        Task<bool> Exist(int id);
        Task<Course> Update(Course course);
        Task<Course> GetByIdAsync(int id);
    }
}
