using EmployeeApp.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;




namespace EmployeeApp.Services
{
    public class JsonFileEmployeeDataService
    {
        public JsonFileEmployeeDataService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "stubData", "employeeData.json"); }
        }

        public Employee GetEmployees()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonConvert.DeserializeObject<Employee>(jsonFileReader.ReadToEnd()); 


                //return JsonSerializer.Deserialize<Employee>(jsonFileReader.ReadToEnd(),
                //    new JsonSerializerOptions
                //    {
                //        PropertyNameCaseInsensitive = true

                //    });
            }
        }
    }
}
