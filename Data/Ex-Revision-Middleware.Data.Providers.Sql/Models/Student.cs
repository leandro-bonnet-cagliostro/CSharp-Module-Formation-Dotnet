using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ex_Revision_Middleware.Data.Providers.Sql.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public short FirstInscriptionYear { get; set; }
        public string Email { get; set; }
    }
}
