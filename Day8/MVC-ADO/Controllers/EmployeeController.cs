using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MVC_ADO.Models;
using System.Collections.Generic;

namespace MVC_ADO.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string connectionString = "server=Pradeep;database=MVCADO;integrated security=true;TrustServerCertificate=true";

        // 1️⃣ Fetch All Employees
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Department = reader.GetString(2),
                            Salary = reader.GetDouble(3)
                        });
                    }
                }
            }

            return View(employees);
        }

        // 2️⃣ Create New Employee - GET
        public IActionResult Create()
        {
            return View();
        }

        // 3️⃣ Create New Employee - POST
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employees (id, name, dept, salary) VALUES (@id, @name, @dept, @salary)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", employee.Id);
                command.Parameters.AddWithValue("@name", employee.Name);
                command.Parameters.AddWithValue("@dept", employee.Department);
                command.Parameters.AddWithValue("@salary", employee.Salary);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                    return RedirectToAction("Index");
            }

            ViewBag.msg = "Failed to add employee.";
            return View();
        }

        // 4️⃣ Fetch Employee Details
        public IActionResult Details(int id)
        {
            Employee employee = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Department = reader.GetString(2),
                            Salary = reader.GetDouble(3)
                        };
                    }
                }
            }

            if (employee == null)
            {
                ViewBag.msg = "Employee not found.";
                return View();
            }

            return View(employee);
        }

        // 5️⃣ Edit Employee - GET
        public IActionResult Edit(int id)
        {
            Employee employee = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Department = reader.GetString(2),
                            Salary = reader.GetDouble(3)
                        };
                    }
                }
            }

            if (employee == null)
            {
                ViewBag.msg = "Employee not found.";
                return View();
            }

            return View(employee);
        }

        // 6️⃣ Edit Employee - POST
        [HttpPost]
        public IActionResult Edit(int id, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employees SET name = @name, dept = @dept, salary = @salary WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", employee.Name);
                command.Parameters.AddWithValue("@dept", employee.Department);
                command.Parameters.AddWithValue("@salary", employee.Salary);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                    return RedirectToAction("Index");
            }

            ViewBag.msg = "Failed to update employee.";
            return View(employee);
        }

        // 7️⃣ Delete Employee - GET
        public IActionResult Delete(int id)
        {
            Employee employee = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Department = reader.GetString(2),
                            Salary = reader.GetDouble(3)
                        };
                    }
                }
            }

            if (employee == null)
            {
                ViewBag.msg = "Employee not found.";
                return View();
            }

            return View(employee);
        }

        // 8️⃣ Delete Employee - POST
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Employees WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                    return RedirectToAction("Index");
            }

            ViewBag.msg = "Failed to delete employee.";
            return View();
        }
    }
}
