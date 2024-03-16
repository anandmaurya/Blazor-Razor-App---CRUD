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
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
    }
}
