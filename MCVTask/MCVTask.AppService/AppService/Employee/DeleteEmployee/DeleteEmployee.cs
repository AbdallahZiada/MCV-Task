using MCVTask.Base.ApiResponse;
using MCVTask.Domain.Employee.Dto;
using MCVTask.Domain.Employee.Repository;
using MediatR;
using System.Runtime.Serialization;

namespace MCVTask.AppService.AppService.Employee.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<ApiResponse<bool>>
    {
        [DataMember]
        public int Id { get; set; }

        public DeleteEmployeeCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, ApiResponse<bool>>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<ApiResponse<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> response = new();

            Domain.Employee.Entity.Employee employee = await _employeeRepository.GetEmployeeById(request.Id);
            employee.DeleteEmployee();

            _employeeRepository.UpdateEmployee(employee);

            if (await _employeeRepository.UnitOfWork.SaveChangesAsync() > 0)
            {
                response.ResponseData = true;
                response.CommandMessage = "Employee Added Successfully";
            }
            else
            {
                response.CommandMessage = "Something Went Wrong";
            }
            return response;
        }
    }
}
