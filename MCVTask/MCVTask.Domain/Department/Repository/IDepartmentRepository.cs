using MCVTask.Base.ApiResponse;
using MCVTask.Domain._SharedKernal;
using MCVTask.Domain.Department.Dto;

namespace MCVTask.Domain.Department.Repository
{
    public interface IDepartmentRepository : IRepository<Department.Entity.Department>
    {
        Task<PageList<DepartmentDto>> GetDepartmentList(int pageIndex);
        Task<DepartmentDto> GetMappedDepartmentById(int id);
        Task<Entity.Department> GetDepartmentById(int id);
        void AddDepartment(Entity.Department department);
        void UpdateDepartment(Entity.Department department);
        Task<bool> HasNoEmployees(int id);
    }
}
