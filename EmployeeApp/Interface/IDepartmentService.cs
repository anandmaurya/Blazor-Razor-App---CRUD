using EmployeeApp.Model;

namespace EmployeeApp.Interface
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetDepartmentAsync();

    }
}
