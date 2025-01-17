using System.ComponentModel.DataAnnotations;

namespace MVCCRUD.Models
{
    public class AddEmployeeViewModel
    {
        public string Name { get; set; }
        public string Dep { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
    }
}
