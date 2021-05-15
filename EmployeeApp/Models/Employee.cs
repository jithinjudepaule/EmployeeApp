using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeApp.Models
{
    public class Employee
    {
        [JsonPropertyName("userId")]
        public string USerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("joiningDate")]
        public DateTime? JoiningDate { get; set; }
        
        [JsonPropertyName("salaryDetails")]
        public Salary SalaryDetails { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
        
        [JsonPropertyName("manager")]
        public string  Manager { get; set; }
       
        [JsonPropertyName("comments")]
        public List<Comment> Comments { get; set; }
       
        [JsonPropertyName("employees")]
        public List<Employee> Employees { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Employee>(this);
       
    }

    public class Comment
    {
        public DateTime? DateCreated { get; set; }
        public string Author { get; set; }
        public string CommentText { get; set; }
    }

    public class Salary
    {
        private int _monthlySalary;
        private int _yearlySalary;
        public int MonthlySalary {
            get
            {
                return _monthlySalary;
            }
            set
            {
                _monthlySalary = Convert.ToInt32(value);
            }
        }
        public int YearlySalary
        {
            get
            {
                return _yearlySalary;
            }
            set
            {
                _yearlySalary = Convert.ToInt32(value);
            }
        }

    }


}
