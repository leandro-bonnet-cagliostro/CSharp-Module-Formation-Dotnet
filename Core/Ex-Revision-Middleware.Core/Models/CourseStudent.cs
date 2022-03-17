using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Core.Models
{
    public class CourseStudent
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime InscriptionDate { get; set; }
        public bool IsPayed { get; set; }
    }
}
