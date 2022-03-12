using MCVTask.Base.ApiResponse;
using MCVTask.Domain.Department.Repository;
using MediatR;
using System.Runtime.Serialization;

namespace MCVTask.AppService.AppService.Department.AddDepartment
{
    public class AddDepartmentCommand : IRequest<ApiResponse<bool>>
    {
        [DataMember]
        public string Name { get; set; }
    }
    public class AddDepartmentHandler : IRequestHandler<AddDepartmentCommand, ApiResponse<bool>>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public AddDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<ApiResponse<bool>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> response = new();

            Domain.Department.Entity.Department department = new();
            department.CreateDepartment(request.Name);

            _departmentRepository.AddDepartment(department);

            if(await _departmentRepository.UnitOfWork.SaveChangesAsync() > 0)
            {
                response.ResponseData = true;
                response.CommandMessage = "Department Added Successfully";
            }
            else
            {
                response.CommandMessage = "Something Went Wrong";
            }
            return response;
        }
    }
}
