using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EmployeeApp.Models;
using EmployeeApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private EmployeeService _employeeService;
        public EmployeesController(EmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Route("getEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeService.GetEmployees();
        }

        [HttpGet]
        [Route("getEmployeeByID")]
        public  Employee getEmployeeByID(string userId)
        {
            return _employeeService.GetEmployeebyId(userId);
        }

        [HttpPost]
        [Route("{userId}/addComments")]
        public async Task<IActionResult> AddComments(string userId,[FromBody]Comment comment)
        {
            var response = await _employeeService.AddComments(userId, comment);
            if (response)
            {
                return Ok("Succesfully Added Comments");
            }

            else
            {
                return NotFound("Unable to find matching record to add comments");
            }
        }

        [HttpDelete]
        [Route("{userId}/deleteComments")]
        public async Task<IActionResult> DeleteCommentsComments(string userId, [FromBody] Comment comment)
        {
            var response = await _employeeService.DeleteComments(userId, comment);
            if (response)
            {
                return Ok("Succesfully Deleted the Comment");
            }

            else
            {
                return NotFound("Unable to find matching record to delete comments");
            }
        }
    }
}
