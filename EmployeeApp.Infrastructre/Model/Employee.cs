using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Infrastructre.Model
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }

    }

    public class EmployeeAdd
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public DateTime DOB { get; set; }
        public char Gender { get; set; }
        public IEnumerable<Department>? Departments { get; set; }

        public EmployeeAdd()
        {
            this.DOB = DateTime.Now.Date.AddYears(-10);
            this.DepartmentID = 1;
            this.Gender = 'M';
        }
    }
}
