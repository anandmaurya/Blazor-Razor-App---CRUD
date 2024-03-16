using Dapper;
using EmployeeApp.Infrastructre;
using EmployeeApp.Infrastructre.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace EmployeeApp.Infrastructre.Services
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

        async Task<EmployeeAdd> IEmployeeService.GetEmployeeByIdAsync(int id)
        {
            EmployeeAdd employees = new EmployeeAdd();
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    string query = @"
                            SELECT 
                                E.ID,
                                E.DepartmentID,
                                E.Name,
                                E.DOB,
                               E.Gender
                            FROM 
                                EmployeeMaster E
                            INNER JOIN 
                                DepartmentMaster D ON E.DepartmentID = D.ID
                            where E.id=@id";
                    return employees = await conn.QueryFirstOrDefaultAsync<EmployeeAdd>(query, new { ID = id });
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

        async Task<string> IEmployeeService.UpdateEmployeesAsync(EmployeeAdd employee)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    string updateQuery = @"
                                    UPDATE EmployeeMaster
                                    SET Name = @Name,
                                        DOB = @DOB,
                                        Gender = @Gender,
                                        DepartmentID = @DepartmentId
                                    WHERE ID = @EmployeeId";

                    int rowsAffected = await conn.ExecuteAsync(updateQuery, new
                    {
                        Name = employee.Name,
                        DOB = employee.DOB,
                        Gender = employee.Gender,
                        DepartmentId = employee.DepartmentID,
                        EmployeeId = employee.ID
                    });

                    if (rowsAffected > 0)
                    {
                        return $"Employee with ID {employee.ID} updated successfully.";
                    }
                    else
                    {
                        return $"Employee with ID {employee.ID} not found or no changes were made.";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error occurred while updating employee: {ex.Message}";
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }


        public async Task<string> DeleteEmployeesAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    string deleteQuery = "DELETE FROM EmployeeMaster WHERE ID = @EmployeeId";

                    int rowsAffected = await conn.ExecuteAsync(deleteQuery, new { EmployeeId = id });

                    if (rowsAffected > 0)
                    {
                        return $"Employee with ID {id} deleted successfully.";
                    }
                    else
                    {
                        return $" Error message: No employee found with the specified {id}";
                    }
                }
                catch (Exception ex)
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

    }
}

