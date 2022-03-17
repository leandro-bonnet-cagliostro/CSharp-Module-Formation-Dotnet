using Ex_Revision_Middleware.Core;
using Ex_Revision_Middleware.Core.Exceptions;
using Ex_Revision_Middleware.Core.Models;
using Ex_Revision_Middleware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Business
{
    public class CourseDomain
    {
        private readonly ICourseRepository courseRepository;

        public CourseDomain(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public async Task<Course> CreateAsync(Course course)
        {
            Validate(course);
            return await this.courseRepository.CreateAsync(course);
        }
        public async Task<IEnumerable<Course>> GetAllAsync(CourseSearchParameters searchParameters)
        {
            return await this.courseRepository.GetAllAsync(searchParameters);
        }

        public async Task<Course> UpdateAsync(Course course)
        {
            if (!await this.courseRepository.Exist(course.Id))
            {
                throw new EntityNotFoundException($"{Constants.ExceptionMessages.EntityNotFound} {typeof(Course)} {course.Id}");
            }
            Validate(course);
            return await this.courseRepository.Update(course);
        }

        private static void Validate(Course course) // private donc ne sort pas de la classe
        {
            if (course == null)
            {
                throw new ArgumentNullException($"{Constants.ExceptionMessages.ObjectNullable} {typeof(Course)}");
            }

            if(course.Id < 0)
            {
                throw new BusinessException($"{Constants.ExceptionMessages.IdShouldBeBetterThan0} {typeof(Course)}");
            }

            if (string.IsNullOrWhiteSpace(course.Title))
            {
                throw new BusinessException(Constants.ExceptionMessages.TitleCannotBeNullOrWhiteSpace);
            }

            if (course.Duration <= 0)
            {
                throw new BusinessException(Constants.ExceptionMessages.DurationShouldBeBetterOrEqualsThan0);
            }

            if(!Regex.IsMatch(course.CodePostal, Constants.ValidationRegex.CodePostalRegex))
            {
                throw new BusinessException(Constants.ExceptionMessages.InvalidCodePostalFormat);
            }
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await this.courseRepository.GetByIdAsync(id);
        }
    }
}
