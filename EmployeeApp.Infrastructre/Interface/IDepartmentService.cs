using EmployeeApp.Infrastructre.Model;

namespace EmployeeApp.Infrastructre
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetDepartmentAsync();

    }
}
