using AutoMapper;
using MCVTask.Domain.Department.Dto;
using MCVTask.Domain.Department.Entity;
using MCVTask.Domain.Employee.Dto;
using MCVTask.Domain.Employee.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCVTask.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
