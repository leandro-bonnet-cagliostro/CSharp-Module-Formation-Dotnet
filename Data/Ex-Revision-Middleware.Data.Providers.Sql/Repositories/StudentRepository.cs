using AutoMapper;
using Ex_Revision_Middleware.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Data.Providers.Sql.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly RevisionEtMiddlewareDbContext context;
        private readonly IMapper mapper;

        public StudentRepository(RevisionEtMiddlewareDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            var studentDb = this.mapper.Map<Sql.Models.Student>(student);
            this.context.Students.Add(studentDb);
            await this.context.SaveChangesAsync();
            student.Id = studentDb.Id;
            return student;
        }
    }
}
