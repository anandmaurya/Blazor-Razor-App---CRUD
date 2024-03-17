using EmployeeApp.Infrastructre;
using EmployeeApp.Infrastructre.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Data;
using System.Reflection.Metadata;

namespace EmployeeApp.Pages
{
    public partial class Index
    {
        private List<Department> Departments { get; set; } = new List<Department>();
        private int SelectedDepartmentId { get; set; }

        private bool IsModalOpen { get; set; }
        private bool IsDeleteModalOpen { get; set; }
        private bool IsViewModalOpen { get; set; }
        private bool IsEdit { get; set; }

        List<Employee> employeeList = new List<Employee>();
        EmployeeAdd employee = new EmployeeAdd();

        private int idToDelete;


        public IConfiguration _configuration { get; }
        [Inject]
        private IEmployeeService _employeeService { get; set; }

        [Inject]
        private IDepartmentService _departmentService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //await JSRuntime.InvokeVoidAsync("jQueryReady");
                await JSRuntime.InvokeVoidAsync("SelectAll");
                await JSRuntime.InvokeAsync<object>("TestDataTablesAdd", "#dtBasicExample");

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
            if(employee.ID != 0)
                await _employeeService.UpdateEmployeesAsync(employee);                        
            else
                await _employeeService.CreateEmployeesAsync(employee);

            await this.GetEmployeeList();
            this.CloseModal();
            ClearForm();
            StateHasChanged();
        }
        protected async void UpdateEmployee()
        {
            var Insert = await _employeeService.UpdateEmployeesAsync(employee);
            await this.GetEmployeeList();
            this.CloseModal();
            StateHasChanged();
        }
        protected async Task GetEmployeeByID(int Id)
        {
            employee = await _employeeService.GetEmployeeByIdAsync(Id);
            IsEdit = true;
            this.ShowEditModal();
        }

        private void ShowConfirmationModal(int id)
        {
            IsDeleteModalOpen = true;
            idToDelete = id;            
        }

        protected async Task DeleteEmployeeByID()
        {
            await _employeeService.DeleteEmployeesAsync(idToDelete);
            StateHasChanged();
            await GetEmployeeList();
            CloseDeleteModal();

        }
        private void ShowEditModal()
        {
            IsModalOpen = true;
        }
        private void ShowModal()
        {
            IsEdit = false;
            IsModalOpen = true;
        }
        private void ClearForm()
        {
            employee = new EmployeeAdd();
        }
        private void CloseDeleteModal()
        {

            IsDeleteModalOpen = false;
        }
        private void CloseModal()
        {
            IsModalOpen = false;
            ClearForm();
            StateHasChanged();
        }
        public async void SelectAll()
        {
            await JSRuntime.InvokeAsync<string>("SelectAll");
        }
        public async void ViewSelectedRows()
        {
            await JSRuntime.InvokeAsync<string>("ViewSelectedRows");
        }
        public async void CloseViewModal()
        {
            await JSRuntime.InvokeAsync<string>("CloseViewModal");
        }
        //public async void DeSelectAll()
        //{
        //    await JSRuntime.InvokeAsync<string>("DeSelectAll");
        //}
    }
}



