using MCVTask.Base.ApiResponse;
using MCVTask.Domain.Department.Dto;
using MCVTask.Domain.Department.Repository;
using MediatR;
using System.Runtime.Serialization;

namespace MCVTask.AppService.AppService.Department.GetDepartment
{
    public class GetDepartmentCommand : IRequest<ApiResponse<DepartmentDto>>
    {
        [DataMember]
        public int Id { get; set; }
        public GetDepartmentCommand(int id)
        {
            Id = id;
        }
    }
    public class GetDepartmentHandler : IRequestHandler<GetDepartmentCommand, ApiResponse<DepartmentDto>>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public GetDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<ApiResponse<DepartmentDto>> Handle(GetDepartmentCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<DepartmentDto> response = new();

            DepartmentDto department = await _departmentRepository.GetMappedDepartmentById(request.Id);

            if (department != null)
            {
                response.ResponseData = department;
                response.CommandMessage = "Department retrieved";
            }
            else
            {
                response.CommandMessage = "Department doesn't exist";
            }
            return response;
        }
    }
}
