using MCVTask.Base.ApiResponse;
using MCVTask.Domain.Department.Repository;
using MediatR;
using System.Runtime.Serialization;

namespace MCVTask.AppService.AppService.Department.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest<ApiResponse<bool>>
    {
        [DataMember]
        public int Id { get; set; }

        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, ApiResponse<bool>>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public DeleteDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<ApiResponse<bool>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> response = new();

            Domain.Department.Entity.Department department = await _departmentRepository.GetDepartmentById(request.Id);
            department.DeleteDepartment();

            _departmentRepository.UpdateDepartment(department);

            if (await _departmentRepository.UnitOfWork.SaveChangesAsync() > 0)
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
