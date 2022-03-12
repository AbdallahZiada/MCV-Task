using MCVTask.Base.ApiResponse;
using MCVTask.Domain.Employee.Dto;
using MCVTask.Domain.Employee.Repository;
using MediatR;
using System.Runtime.Serialization;

namespace MCVTask.AppService.AppService.Employee.EmployeeList
{
    public class EmployeeListCommand : IRequest<PageList<EmployeeDto>>
    {
        [DataMember]
        public int PageIndex { get; set; }
        public EmployeeListCommand(int pageIndex)
        {
            PageIndex = pageIndex;
        }
    }
    public class EmployeeListHandler : IRequestHandler<EmployeeListCommand, PageList<EmployeeDto>>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public EmployeeListHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<PageList<EmployeeDto>> Handle(EmployeeListCommand request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetEmployeeList(request.PageIndex);
        }
    }
}
