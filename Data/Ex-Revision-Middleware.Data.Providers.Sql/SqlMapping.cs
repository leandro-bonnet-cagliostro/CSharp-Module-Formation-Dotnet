using AutoMapper;
using Ex_Revision_Middleware.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Data.Providers.Sql
{
    public class SqlMapping : Profile
    {
        public SqlMapping()
        {
            // Sql.Models.Course,Core.Models.Course
            this.CreateMap<Core.Models.Course,Sql.Models.Course>()
                .ForMember(m => m.CodePostal, opt => opt.MapFrom(src => short.Parse(src.CodePostal)));

            this.CreateMap<Sql.Models.Course, Core.Models.Course>()
                .ForMember(m => m.CodePostal, opt => opt.MapFrom(src => src.CodePostal.ToString()));


            this.CreateMap<Student, Sql.Models.Student>().ReverseMap();

            this.CreateMap<Sql.Models.User, Core.Models.User>().ReverseMap();

            this.CreateMap<Sql.Models.Role, Core.Models.Role>().ReverseMap();
        }
    }
}
