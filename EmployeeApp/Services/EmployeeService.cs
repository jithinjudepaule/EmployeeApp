using EmployeeApp.DataAccessors;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    
namespace EmployeeApp.Services
{
    public class EmployeeService
    {
        private SqlHelper _sqlHelper;
        public EmployeeService(SqlHelper sqlHelper)
        {
            this._sqlHelper = sqlHelper;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _sqlHelper.GetEmployeeData();

        }

        public Employee GetEmployeebyId(string userId)
        {
            return _sqlHelper.GetEmployeeDataById(userId);

        }

        public async Task<bool> AddComments(string userId, Comment comment)
        {
            var response = await _sqlHelper.AddComments(userId, comment);

            return response;
        }

        public async Task<bool> DeleteComments(string userId, Comment comment)
        {
            var response = await _sqlHelper.DeleteComments(userId, comment);

            return response;
        }
    }
}
