using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ex_Revision_Middleware.Data.Providers.Sql.Models
{
    [Table("CourseStudent")]
    public class CourseStudent
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime InscriptionDate { get; set; }
        public bool IsPayed { get; set; }
    }
}
