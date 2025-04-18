using CRUD_USING_STRUCTURE.Models;
using System.Data.SqlClient;
using CRUD_USING_STRUCTURE.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Azure.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CRUD_USING_STRUCTURE.Services
{
    class EmployeeServices
    {
        private String _connectionString;
        public EmployeeServices(String connectionString)
        {
            _connectionString = connectionString;
        }


        // functions for the crud aaplication 
        public List<Employee> GetAll()
        {
            List<Employee> employee = new();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlQuery.GettingAllEmployee, connection))
                    {
                        using (SqlDataReader myreader = command.ExecuteReader())
                        {
                            while (myreader.Read())
                            {
                                employee.Add(new Employee
                                {
                                    EmployeeID = (int)myreader["EmployeeId"],
                                    FirstName = (string)myreader["FirstName"],
                                    LastName = (string)myreader["LastName"],
                                    Email = (string)myreader["Email"],
                                    Position = (string)myreader["Position"],
                                    Salary = myreader["Salary"] != DBNull.Value ? Convert.ToDecimal(myreader["Salary"]) : 0m

                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching employees: {ex.Message}");
            }
            return employee;

        }

        public void Add(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SqlQuery.InsertIntoEmployee, connection))
                    try
                    {
                        command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@Email", employee.Email);
                        command.Parameters.AddWithValue("@Position", employee.Position);
                        command.Parameters.AddWithValue("@Salary", employee.Salary);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding employee: {ex.Message}");
                    }

            }
        }

        public void Update(Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlQuery.UpdateEmployee, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                        command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@Email", employee.Email);
                        command.Parameters.AddWithValue("@Position", employee.Position);
                        command.Parameters.AddWithValue("@Salary", employee.Salary);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee: {ex.Message}");
            }
        }


        public void Delete(Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(SqlQuery.DeleteEmployee, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
            }
        }

        public Employee GetEmployee(int employeeID)
        {
            Employee employee = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlQuery.GetEmployeebyID, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", employeeID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employee = new Employee
                                {
                                    EmployeeID = (int)reader["EmployeeId"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Email = (string)reader["Email"],
                                    Position = (string)reader["Position"],
                                    Salary = (decimal)reader["Salary"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding employee: {ex.Message}");
            }

            return employee;
        }

        public List<Employee> SearchEmployees(String Keyword)
        {
            var employees = GetAll();
            Keyword = Keyword.ToLower();

            return employees.Where(e =>
            
                e.FirstName.ToLower().Contains(Keyword) ||
                e.LastName.ToLower().Contains(Keyword) ||
                e.Email.ToLower().Contains(Keyword) ||
                e.Position.ToLower().Contains(Keyword)
            ).ToList();
        }
    }
}


