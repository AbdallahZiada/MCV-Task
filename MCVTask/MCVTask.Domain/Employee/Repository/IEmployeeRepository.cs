using MCVTask.Base.ApiResponse;
using MCVTask.Domain._SharedKernal;
using MCVTask.Domain.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCVTask.Domain.Employee.Repository
{
    public interface IEmployeeRepository : IRepository<Employee.Entity.Employee>
    {
        Task<PageList<EmployeeDto>> GetEmployeeList(int pageIndex);
        Task<EmployeeDto> GetMappedEmployeeById(int id);
        Task<Entity.Employee> GetEmployeeById(int id);
        void AddEmployee(Entity.Employee Employee);
        void UpdateEmployee(Entity.Employee Employee);
        Task<bool> IsUniqueEmployeeId(string employeeId, int targetId = 0);
    }
}
