using System;
using System.ComponentModel.DataAnnotations;

namespace Async_Demo.Model
{
    public class Employee
    {
        [Key]
        public int Employee_PK { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmpCode { get; set; }
        [Required]
        public int Gender { get; set; } // 1 - Male, 2 - Female
        [Required]
        public DateTime DoB { get; set; }
        public decimal? Salary { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        public DateTime? ResignDate { get; set; }
    }
}
