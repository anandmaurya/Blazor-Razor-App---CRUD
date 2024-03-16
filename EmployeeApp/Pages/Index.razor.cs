using EmployeeApp.Infrastructre;
using EmployeeApp.Infrastructre.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EmployeeApp.Pages
{
    public partial class Index
    {
        private List<Department> Departments { get; set; } = new List<Department>();
        private int SelectedDepartmentId { get; set; }
        
        private bool IsModalOpen { get; set; }

        List<Employee> employeeList = new List<Employee>();
        EmployeeAdd employee = new EmployeeAdd();

        public IConfiguration _configuration { get; }
        [Inject]
        private IEmployeeService _employeeService { get; set; }

        [Inject]
        private IDepartmentService _departmentService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("SelectAll");
            }
        }

       
        protected override async Task OnInitializedAsync()
        {
            await this.GetEmployeeList();
            Departments = await _departmentService.GetDepartmentAsync();
        }

        protected async Task GetEmployeeList()
        {
            employeeList = await _employeeService.GetEmployeesAsync();
        }
        protected async void CreateEmployee()
        {
           var Insert= await _employeeService.CreateEmployeesAsync(employee);
            //NavigationManager.NavigateTo("Employees");
        }

        private void ShowModal()
        {
            IsModalOpen = true;
        }

        private void CloseModal()
        {
            IsModalOpen = false;
        }
        public async void SelectAll()
        {
            await JS.InvokeAsync<string>("SelectAll");
        }
    }


}
