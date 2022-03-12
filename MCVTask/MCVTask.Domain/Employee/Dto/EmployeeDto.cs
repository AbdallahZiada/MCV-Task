using MCVTask.Base.Dto;
using MCVTask.Domain.Department.Dto;
using MCVTask.Domain.Department.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCVTask.Domain.Employee.Dto
{
    public class EmployeeDto : BaseDto<int>
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string JobTitle { get; set; }
        public DateTime HiringDate { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
