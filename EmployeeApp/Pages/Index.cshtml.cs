using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApp.Models;
using EmployeeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EmployeeApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileEmployeeDataService JsonService;
        public IEnumerable<Employee> Employees { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, JsonFileEmployeeDataService jsonService)
        {
            _logger = logger;
            this.JsonService = jsonService;
        }

        public void OnGet()
        {
            Employees = JsonService.GetEmployees().Employees;
        }
    }
}
