using AutoMapper;
using MCVTask.Base.ApiResponse;
using MCVTask.Domain._SharedKernal;
using MCVTask.Domain.Employee.Dto;
using MCVTask.Domain.Employee.Entity;
using MCVTask.Domain.Employee.Repository;
using MCVTask.Infrastructure.Base;
using MCVTask.Infrastructure.Context;

namespace MCVTask.Infrastructure.Repositories
{
    internal class EmployeeRepository : EntityRepository<Employee>, IEmployeeRepository
    {
        public IUnitOfWork UnitOfWork => AppDbContext;
        public EmployeeRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        { }
        public async Task<PageList<EmployeeDto>> GetEmployeeList(int pageIndex)
        {
            PageList<EmployeeDto> result = new();
            List<Employee> employeeList = await GetPageAsync(pageIndex, 1000, x => x.IsDeleted == false, x => x.Id, "Department");
            if (employeeList?.Count > default(int))
            {
                int totalCount = await GetCountAsync(x => x.IsDeleted);
                result.SetResult(totalCount, Mapper.Map<List<Employee>, List<EmployeeDto>>(employeeList));
            }
            return result;
        }
        public async Task<EmployeeDto> GetMappedEmployeeById(int id)
        {
            return Mapper.Map<EmployeeDto>(await FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id, "Department"));
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            return await FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
        }
        public void AddEmployee(Employee employee)
        {
            CreateAsyn(employee);
        }
        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }

        public async Task<bool> IsUniqueEmployeeId(string employeeId, int targetId = 0)
        {
            return (await GetCountAsync(x => x.IsDeleted == false && x.EmployeeId == employeeId && (x.Id != targetId || targetId == 0)) == 0);
        }
    }
}
