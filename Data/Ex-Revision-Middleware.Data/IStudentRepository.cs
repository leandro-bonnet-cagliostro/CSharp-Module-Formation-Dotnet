using Ex_Revision_Middleware.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Data
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student student);
    }
}
