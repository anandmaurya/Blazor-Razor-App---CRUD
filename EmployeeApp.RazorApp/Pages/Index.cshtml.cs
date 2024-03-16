using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeApp.Infrastructre;
using EmployeeApp.Infrastructre.Model;
using EmployeeApp.RazorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace EmployeeApp.RazorApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeService _employee;
        private readonly IDepartmentService _department;
        private readonly IRazorRenderService _renderService;
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public IndexModel(ILogger<IndexModel> logger, IEmployeeService employee, IDepartmentService department, IRazorRenderService renderService, IMapper mapper)
        {
            _logger = logger;
            _employee = employee;
            _department = department;
            _renderService = renderService;
            _mapper = mapper;
        }
        public IEnumerable<Employee> Employees { get; set; }
        public void OnGet()
        {
        }
        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            Employees = await _employee.GetEmployeesAsync();
            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<Employee>>(ViewData, Employees)
            };
        }

        /// <summary>
        /// On edit and create employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> OnGetCreateOrEditAsync(int id = 0)
        {
            if (id == 0)
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", new EmployeeAdd { Departments = await _department.GetDepartmentAsync() }) });
            else
            {
                var employee = await _employee.GetEmployeeByIdAsync(id);                
                var departments = await _department.GetDepartmentAsync();
                employee.Departments = departments;
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", employee) });
            }
        }

        /// <summary>
        /// This action used for create and update both
        /// Used partial view concept to render the form in modal popup
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, EmployeeAdd employee)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await _employee.CreateEmployeesAsync(employee);                    
                }
                else
                {
                    employee.ID = id;
                    await _employee.UpdateEmployeesAsync(employee);
                }
                Employees = await _employee.GetEmployeesAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", Employees);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_CreateOrEdit", employee);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
        /// <summary>
        /// Delete employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            await _employee.DeleteEmployeesAsync(id);
            
            Employees = await _employee.GetEmployeesAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", Employees);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}
