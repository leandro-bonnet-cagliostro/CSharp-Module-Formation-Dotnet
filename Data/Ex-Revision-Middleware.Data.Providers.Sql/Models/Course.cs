using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ex_Revision_Middleware.Data.Providers.Sql.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public short CodePostal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
