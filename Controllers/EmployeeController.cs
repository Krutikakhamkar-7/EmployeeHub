using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using System.Data;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly CountryApiService _countryApiService;

        public EmployeeController(
            IConfiguration configuration,
            CountryApiService countryApiService)
        {
            _configuration = configuration;
            _countryApiService = countryApiService;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));
            }
        }

        // ================= INDEX =================
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

                var allEmployees = db.Query<Employee>("SELECT * FROM Employees");
                return View(allEmployees);
            }
        }

        // ================= CREATE (GET) =================
        public async Task<IActionResult> Create()
        {
            ViewBag.Countries = await _countryApiService.GetCountries();
            return View();
        }

        // ================= CREATE (POST) =================
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Countries = await _countryApiService.GetCountries();
                return View(employee);
            }

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

        // ================= TEST =================
        public IActionResult Test()
        {
            return Content("Controller Working");
        }

        // ================= API =================
        [HttpGet]
        public async Task<JsonResult> GetStates(string countryCode)
        {
            var states = await _countryApiService.GetStates(countryCode);
            return Json(states);
        }

        [HttpGet]
        public async Task<JsonResult> GetCities(string countryCode, string stateCode)
        {
            var cities = await _countryApiService.GetCities(countryCode, stateCode);
            return Json(cities);
        }

        // ================= EDIT (GET) =================
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

        // ================= EDIT (POST) =================
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

        // ================= DELETE =================
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