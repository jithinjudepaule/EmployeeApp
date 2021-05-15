using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Models
{
    public class Employee
    {
        public int Name { get; set; }
        public DateTime? JoiningDate { get; set; }
        public Salary SalaryDetails { get; set; }
        public string Address { get; set; }
        public string  Manager { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public DateTime? DateCreated { get; set; }
        public string Author { get; set; }
        public string CommentText { get; set; }
    }

    public class Salary
    {
        public int MonthlySalary { get; set; }
        public int YearlySalary { get; set; }

    }
}
