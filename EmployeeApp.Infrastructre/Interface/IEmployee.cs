using EmployeeApp.Infrastructre.Model;

namespace EmployeeApp.Infrastructre
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetEmployeesAsync();
        public Task<EmployeeAdd> GetEmployeeByIdAsync(int id);
        public Task<string> CreateEmployeesAsync(EmployeeAdd employee);
        public Task<string> UpdateEmployeesAsync(EmployeeAdd employee);
        public Task<string> DeleteEmployeesAsync(int id);
    }
}
