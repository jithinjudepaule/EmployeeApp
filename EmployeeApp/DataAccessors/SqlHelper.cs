using EmployeeApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.DataAccessors
{
    public class SqlHelper
    {
        private static string _connectionString;
        private SqlConnection _sqlConnection;
        public SqlHelper()
        {
            _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EmployeeDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _sqlConnection = new SqlConnection(_connectionString);

        }
        public IEnumerable<Employee> GetEmployeeData()
        {
            if (_sqlConnection.State != ConnectionState.Open)
            {
                _sqlConnection.Open();
            }
            List<Employee> employees = new List<Employee>();
            Employee employee;
            string oString = "Select * from EmployeeDetails e inner join EmployeeComments c on e.UserId=c.UserId";

            SqlCommand oCmd = new SqlCommand(oString, _sqlConnection);

            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    if (employees.Any(x => x.UserId == oReader["UserId"].ToString()))
                    {
                        Employee tempEmployee = employees.First(x => x.UserId == oReader["UserId"].ToString());
                        tempEmployee.Comments.Add(new Comment
                        {
                            Author = oReader["Author"].ToString(),
                            CommentText = oReader["Text"].ToString(),
                            DateCreated = Convert.ToDateTime(oReader["CreationDate"])
                        });
                    }
                    else
                    {
                        employee = new Employee();
                        employee.UserId = oReader["UserId"].ToString();
                        employee.Name = oReader["Name"].ToString();
                        employee.JobTitle = oReader["JobTitle"].ToString();
                        employee.JoiningDate = Convert.ToDateTime(oReader["JoiningDate"]);
                        employee.Manager = oReader["Manager"].ToString();
                        employee.Address = oReader["Address"].ToString();
                        employee.JobTitle = oReader["Name"].ToString();
                        employee.SalaryDetails = new Salary
                        {
                            MonthlySalary = Convert.ToInt32(oReader["MonthlySalary"].ToString()),
                            YearlySalary = Convert.ToInt32(oReader["YearlySalary"].ToString())
                        };
                        
                        employee.Comments = new List<Comment>();
                        employee.Comments.Add(new Comment
                        {
                            Author = oReader["Author"].ToString(),
                            CommentText = oReader["Text"].ToString(),
                            DateCreated = Convert.ToDateTime(oReader["CreationDate"])
                        });
                        employees.Add(employee);
                    }
                }

                _sqlConnection.Close();
            }
            return employees;
        }

        public Employee GetEmployeeDataById(string userId)
        { 
          if (_sqlConnection.State != ConnectionState.Open)
            {
                _sqlConnection.Open();
            }
            Employee employee=null;
            string oString = $"Select * from EmployeeDetails e inner join EmployeeComments c on e.UserId=c.UserId where e.UserId={userId}";
            int count = 0;
            SqlCommand oCmd = new SqlCommand(oString, _sqlConnection);

            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    if (count>0)
                    {
                        employee.Comments.Add(new Comment
                        {
                            Author = oReader["Author"].ToString(),
                            CommentText = oReader["Text"].ToString(),
                            DateCreated = Convert.ToDateTime(oReader["CreationDate"])
                        });
                    }
                    else
                    {
                        employee = new Employee();
                        employee.UserId = oReader["UserId"].ToString();
                        employee.Name = oReader["Name"].ToString();
                        employee.JobTitle = oReader["JobTitle"].ToString();
                        employee.JoiningDate = Convert.ToDateTime(oReader["JoiningDate"]);
                        employee.Manager = oReader["Manager"].ToString();
                        employee.Address = oReader["Address"].ToString();
                        employee.JobTitle = oReader["Name"].ToString();
                        employee.SalaryDetails = new Salary
                        {
                            MonthlySalary = Convert.ToInt32(oReader["MonthlySalary"].ToString()),
                            YearlySalary = Convert.ToInt32(oReader["YearlySalary"].ToString())
                        };

                        employee.Comments = new List<Comment>();
                        employee.Comments.Add(new Comment
                        {
                            Author = oReader["Author"].ToString(),
                            CommentText = oReader["Text"].ToString(),
                            DateCreated = Convert.ToDateTime(oReader["CreationDate"])
                        });
                    }
                    
                    count++;
                }
                _sqlConnection.Close();
            }
            return employee;
    }
        public async Task<bool> AddComments(string userId,Comment commentData)
        {
            if (_sqlConnection.State != ConnectionState.Open)
            {
                _sqlConnection.Open();
            }
          
            string oString = $"Insert into [dbo].[EmployeeComments] (UserId,Author,CreationDate,Text) values ({userId}, {commentData.Author}, {commentData.DateCreated}, {commentData.CommentText}) where EmployeeComments.UserId={userId}";
           
            SqlCommand oCmd = new SqlCommand(oString, _sqlConnection);
            int rowsAffected= await oCmd.ExecuteNonQueryAsync();
            _sqlConnection.Close();
            return rowsAffected == 1 ? true : false;
        }
        public async Task<bool>  DeleteComments(string userId, Comment commentData)
        {
            if (_sqlConnection.State != ConnectionState.Open)
            {
                _sqlConnection.Open();
            }

            string oString = $"Delete from [dbo].[EmployeeComments]  where EmployeeComments.Text={commentData.CommentText} and EmployeeComments.UserId={userId};";

            SqlCommand oCmd = new SqlCommand(oString, _sqlConnection);
            int rowsAffected = await oCmd.ExecuteNonQueryAsync();
            _sqlConnection.Close();
            return rowsAffected == 1 ? true : false;

        }




    }
}
