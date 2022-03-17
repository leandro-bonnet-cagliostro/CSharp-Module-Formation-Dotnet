using Ex_Revision_Middleware.Core.Models;
using Ex_Revision_Middleware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Business
{
    public class StudentDomain
    {
        private readonly IStudentRepository studentRepository;

        public StudentDomain(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            return await this.studentRepository.CreateAsync(student);
        }
    }
}
