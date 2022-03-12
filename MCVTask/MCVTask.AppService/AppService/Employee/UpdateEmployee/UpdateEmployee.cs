using MCVTask.Base.ApiResponse;
using MCVTask.Domain.Department.Repository;
using MCVTask.Domain.Employee.Dto;
using MCVTask.Domain.Employee.Repository;
using MediatR;
using System.Runtime.Serialization;

namespace MCVTask.AppService.AppService.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<ApiResponse<bool>>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string EmployeeId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public DateTime HiringDate { get; set; }
        [DataMember]
        public int DepartmentId { get; set; }
    }
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, ApiResponse<bool>>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public readonly IDepartmentRepository _departmentRepository;
        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<ApiResponse<bool>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> response = new();

            Domain.Department.Entity.Department department = await _departmentRepository.GetDepartmentById(request.DepartmentId);

            Domain.Employee.Entity.Employee employee = await _employeeRepository.GetEmployeeById(request.Id);
            employee.UpdateEmployee(request.EmployeeId, request.Name, request.PhoneNumber, request.BirthDate, request.Title, request.HiringDate, department);

            _employeeRepository.UpdateEmployee(employee);

            if (await _employeeRepository.UnitOfWork.SaveChangesAsync() > 0)
            {
                response.ResponseData = true;
                response.CommandMessage = "Employee Updated Successfully";
            }
            else
            {
                response.CommandMessage = "Something Went Wrong";
            }
            return response;
        }
    }
}
