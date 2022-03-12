using AutoMapper;
using MCVTask.Base.ApiResponse;
using MCVTask.Domain._SharedKernal;
using MCVTask.Domain.Department.Dto;
using MCVTask.Domain.Department.Entity;
using MCVTask.Domain.Department.Repository;
using MCVTask.Infrastructure.Base;
using MCVTask.Infrastructure.Context;

namespace MCVTask.Infrastructure.Repositories
{
    internal class DepartmentRepository : EntityRepository<Department>, IDepartmentRepository
    {
        public IUnitOfWork UnitOfWork => AppDbContext;
        public DepartmentRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        { }
        public async Task<PageList<DepartmentDto>> GetDepartmentList(int pageIndex)
        {
            PageList<DepartmentDto> result = new();
            List<Department> departmentList = await GetPageAsync(pageIndex, 1000, x => x.IsDeleted == false, x => x.Id);
            if (departmentList?.Count > default(int))
            {
                int totalCount = await GetCountAsync(x => x.IsDeleted);
                result.SetResult(totalCount, Mapper.Map<List<Department>, List<DepartmentDto>>(departmentList));
            }
            return result;
        }
        public async Task<DepartmentDto> GetMappedDepartmentById(int id)
        {
            return Mapper.Map<DepartmentDto>(await FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id));
        }
        public async Task<Department> GetDepartmentById(int id)
        {
            return await FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
        }
        public void AddDepartment(Department department)
        {
            CreateAsyn(department);
        }
        public void UpdateDepartment(Department department)
        {
            Update(department);
        }

        public async Task<bool> HasNoEmployees(int id)
        {
            return !(await FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id, "Employees")).Employees.Any();
        }
    }
}
