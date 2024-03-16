﻿using Dapper;
using EmployeeApp.Interface;
using EmployeeApp.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace EmployeeApp.Services
{
    public class DepartmentService:IDepartmentService
    {
        public IConfiguration _configuration { get; }
        public string _connectionString { get; }

        public DepartmentService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("LocalDB");
        }

        public async Task<List<Department>> GetDepartmentAsync()
        {
            IEnumerable<Department> department = new List<Department>();
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    string query = @"SELECT  ID, DepartmentName from DepartmentMaster Where IsActive=1";
                    department = await conn.QueryAsync<Department>(query);
                    return department.ToList();
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
