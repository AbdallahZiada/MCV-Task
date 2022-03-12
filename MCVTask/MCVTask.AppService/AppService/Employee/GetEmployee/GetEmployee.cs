using MCVTask.Base.ApiResponse;
using MCVTask.Domain.Employee.Dto;
using MCVTask.Domain.Employee.Repository;
using MediatR;
using System.Runtime.Serialization;

namespace MCVTask.AppService.AppService.Employee.GetEmployee
{
    public class GetEmployeeCommand : IRequest<ApiResponse<EmployeeDto>>
    {
        [DataMember]
        public int Id { get; set; }
        public GetEmployeeCommand(int id)
        {
            Id = id;
        }
    }
    public class GetEmployeeHandler : IRequestHandler<GetEmployeeCommand, ApiResponse<EmployeeDto>>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<ApiResponse<EmployeeDto>> Handle(GetEmployeeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<EmployeeDto> response = new();

            EmployeeDto employee = await _employeeRepository.GetMappedEmployeeById(request.Id);

            if (employee != null)
            {
                response.ResponseData = employee;
                response.CommandMessage = "Employee retrieved";
            }
            else
            {
                response.CommandMessage = "Employee doesn't exist";
            }
            return response;
        }
    }
}
