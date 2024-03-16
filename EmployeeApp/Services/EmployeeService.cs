using Dapper;
using EmployeeApp.Interface;
using EmployeeApp.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace EmployeeApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IConfiguration _configuration { get; }
        public string _connectionString { get; }

        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("LocalDB");
        }

        async Task<List<Employee>> IEmployeeService.GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = new List<Employee>();
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    string query = @"
                            SELECT 
                                E.ID,                                                              
                                E.Name,
                                D.DepartmentName,
                                E.DOB,
                                CASE 
                                    WHEN E.Gender='M' THEN 'Male'
                                    WHEN E.Gender='F' THEN 'Female'
                                END AS Gender
                            FROM 
                                EmployeeMaster E
                            INNER JOIN 
                                DepartmentMaster D ON E.DepartmentID = D.ID";
                    employees = await conn.QueryAsync<Employee>(query);
                    return employees.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        async Task<Employee> IEmployeeService.GetEmployeeByIdAsync(int id)
        {
            Employee employees = new Employee();
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    string query = @"
                            SELECT 
                                E.ID,
                                D.DepartmentName,
                                E.Name,
                                E.DOB,
                               E.Gender
                            FROM 
                                EmployeeMaster E
                            INNER JOIN 
                                DepartmentMaster D ON E.DepartmentID = D.ID
                            where E.id={id}";
                    return employees = await conn.QueryFirstOrDefaultAsync<Employee>(query);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        async Task<string> IEmployeeService.CreateEmployeesAsync(EmployeeAdd employee)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    string insertQuery = $@"
                INSERT INTO EmployeeMaster (Name, DOB, Gender, DepartmentID)
                VALUES (@Name, @DOB, @Gender, @DepartmentId);

                SELECT CAST(SCOPE_IDENTITY() as int)";

                    int newEmployeeId = await conn.QueryFirstOrDefaultAsync<int>(insertQuery, new
                    {
                        Name = employee.Name,
                        DOB = employee.DOB,
                        Gender = employee.Gender,
                        DepartmentId = employee.DepartmentID
                    });

                    return $"Employee with ID {newEmployeeId} created successfully.";
                }
                catch (Exception ex)
                {
                    return $"Error occurred while creating employee: {ex.Message}";
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        Task<Employee> IEmployeeService.UpdateEmployeesAsync(int id, Employee employee)
        {
            throw new NotImplementedException();
        }

        Task IEmployeeService.DeleteEmployeesAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

