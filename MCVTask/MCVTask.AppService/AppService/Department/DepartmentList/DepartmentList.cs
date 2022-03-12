using MCVTask.Base.ApiResponse;
using MCVTask.Domain.Department.Dto;
using MCVTask.Domain.Department.Repository;
using MediatR;
using System.Runtime.Serialization;

namespace MCVTask.AppService.AppService.Department.DepartmentList
{
    public class DepartmentListCommand : IRequest<PageList<DepartmentDto>>
    {
        [DataMember]
        public int PageIndex { get; set; }
        public DepartmentListCommand(int pageIndex)
        {
            PageIndex = pageIndex;
        }
    }
    public class DepartmentListHandler : IRequestHandler<DepartmentListCommand, PageList<DepartmentDto>>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public DepartmentListHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<PageList<DepartmentDto>> Handle(DepartmentListCommand request, CancellationToken cancellationToken)
        {
            return await _departmentRepository.GetDepartmentList(request.PageIndex);
        }
    }
}
