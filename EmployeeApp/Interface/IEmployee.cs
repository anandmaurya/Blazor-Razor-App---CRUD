using EmployeeApp.Model;

namespace EmployeeApp.Interface
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetEmployeesAsync();
        public Task<Employee> GetEmployeeByIdAsync(int id);
        public Task<string> CreateEmployeesAsync(EmployeeAdd employee);
        public Task<Employee> UpdateEmployeesAsync(int id,Employee employee);
        public Task DeleteEmployeesAsync(int id);
    }
}
