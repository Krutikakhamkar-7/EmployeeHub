using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using EmployeeManagement.Models;
using System.Data;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public IActionResult Index(string search)
{
    using (IDbConnection db = Connection)
    {
        if (!string.IsNullOrEmpty(search))
        {
            var employees = db.Query<Employee>(
                "SELECT * FROM Employees WHERE FirstName LIKE @search OR LastName LIKE @search",
                new { search = "%" + search + "%" });

            return View(employees);
        }

        var allEmployees = db.Query<Employee>(
            "SELECT * FROM Employees");

        return View(allEmployees);
    }
}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            using (IDbConnection db = Connection)
            {
                string sql = @"
                INSERT INTO Employees
                (
                    FirstName,
                    LastName,
                    DOB,
                    Country,
                    State,
                    City,
                    Qualification,
                    Email,
                    PhoneNo
                )
                VALUES
                (
                    @FirstName,
                    @LastName,
                    @DOB,
                    @Country,
                    @State,
                    @City,
                    @Qualification,
                    @Email,
                    @PhoneNo
                )";

                db.Execute(sql, employee);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            using (IDbConnection db = Connection)
            {
                var employee = db.QueryFirstOrDefault<Employee>(
                    "SELECT * FROM Employees WHERE EmployeeId=@id",
                    new { id });

                return View(employee);
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            using (IDbConnection db = Connection)
            {
                string sql = @"
                UPDATE Employees
                SET
                    FirstName=@FirstName,
                    LastName=@LastName,
                    DOB=@DOB,
                    Country=@Country,
                    State=@State,
                    City=@City,
                    Qualification=@Qualification,
                    Email=@Email,
                    PhoneNo=@PhoneNo
                WHERE EmployeeId=@EmployeeId";

                db.Execute(sql, employee);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using (IDbConnection db = Connection)
            {
                db.Execute(
                    "DELETE FROM Employees WHERE EmployeeId=@id",
                    new { id });
            }

            return RedirectToAction("Index");
        }
    }
}