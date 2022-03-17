using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ex_Revision_Middleware.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Data.Providers.Sql.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly RevisionEtMiddlewareDbContext context;
        private readonly IMapper mapper;

        public CourseRepository(RevisionEtMiddlewareDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Course> CreateAsync(Course course)
        {
            // this.context.RoleClaims
            var courseDb = this.mapper.Map<Sql.Models.Course>(course);
            this.context.Courses.Add(courseDb);
            await this.context.SaveChangesAsync();
            course.Id = courseDb.Id;
            return course;
            // mapper l'objet core en objet sql
            // this.context.Courses.Add();
            // sauvegarder
            // retourner l'objet avec son id
        }

        public async Task<IEnumerable<Course>> GetAllAsync(CourseSearchParameters searchParameters)
        {
            var coursesQuery = this.context.Courses.AsNoTracking();
            coursesQuery = this.ApplyFilters(coursesQuery, searchParameters);

            //throw new System.NotImplementedException();
            return this.mapper.Map<IEnumerable<Course>>(await coursesQuery.ToListAsync());
            //return this.mapper.Map<IEnumerable<Course>>(await );
            //var listCore = await this.context.
            //var listCore = await this.courseDomain.GetAllAsync();
            //return this.Ok(this.mapper.Map<IEnumerable<ArticleListViewModel>>(listCore));
        }

        private IQueryable<Models.Course> ApplyFilters(IQueryable<Models.Course> coursesQuery, 
                                                        CourseSearchParameters searchParameters)
        {
            if (!string.IsNullOrWhiteSpace(searchParameters.Title))
            {
                coursesQuery = coursesQuery.Where(c => c.Title.Contains(searchParameters.Title));
            }

            if(searchParameters.Duration.HasValue) // on peut aussi tester avec > ou <= searchParameters.Duration
            {
                // .value car int
                coursesQuery = coursesQuery.Where(c => c.Duration == searchParameters.Duration.Value);
            }

            if (searchParameters.Price.HasValue) // on peut aussi tester avec > ou <=
            {
                // .value car int
                coursesQuery = coursesQuery.Where(c => c.Price == searchParameters.Price.Value);
            }

            if (searchParameters.StartDate.HasValue) // on peut aussi tester avec > ou <=
            {
                // .value car int
                coursesQuery = coursesQuery.Where(c => 
                                        c.StartDate.Year == searchParameters.StartDate.Value.Year &&
                                        c.StartDate.Month == searchParameters.StartDate.Value.Month &&
                                        c.StartDate.Day == searchParameters.StartDate.Value.Day
                                        );
            }

            return coursesQuery;
        }

        public async Task<bool> Exist(int id)
        {
            // any retourne un boolean si la condition est respectée
            return await this.context.Courses.AsNoTracking().AnyAsync(c => c.Id == id);
        }

        public async Task<Course> Update(Course course)
        {
            var courseDb = this.mapper.Map<Models.Course>(course);
            this.context.Courses.Attach(courseDb);
            this.context.Entry(courseDb).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            var courseDb = await this.context.Courses
                                .AsNoTracking()
                                .FirstOrDefaultAsync(c => c.Id == id);
            return this.mapper.Map<Course>(courseDb);
        }
    }
}
