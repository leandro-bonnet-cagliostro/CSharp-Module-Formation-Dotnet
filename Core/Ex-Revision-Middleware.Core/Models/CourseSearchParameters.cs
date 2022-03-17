using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Core.Models
{
    public class CourseSearchParameters
    {
        public string Title { get; set; }
        public int? Duration { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
