using System.ComponentModel.DataAnnotations;

namespace MVCCRUD.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Dep { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public double Salary { get; set; }

    }
}
