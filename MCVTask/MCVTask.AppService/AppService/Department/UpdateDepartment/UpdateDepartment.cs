using MCVTask.Base.ApiResponse;
using MCVTask.Domain.Department.Repository;
using MediatR;
using System.Runtime.Serialization;

namespace MCVTask.AppService.AppService.Department.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest<ApiResponse<bool>>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, ApiResponse<bool>>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public UpdateDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<ApiResponse<bool>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> response = new();

            Domain.Department.Entity.Department department = await _departmentRepository.GetDepartmentById(request.Id);
            department.UpdateDepartment(request.Name);

            _departmentRepository.UpdateDepartment(department);

            if (await _departmentRepository.UnitOfWork.SaveChangesAsync() > 0)
            {
                response.ResponseData = true;
                response.CommandMessage = "Department Updated Successfully";
            }
            else
            {
                response.CommandMessage = "Something Went Wrong";
            }
            return response;
        }
    }
}
