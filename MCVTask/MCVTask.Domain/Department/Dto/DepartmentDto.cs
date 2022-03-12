using MCVTask.Base.Dto;
using MCVTask.Base.Entity;
using MCVTask.Domain.Employee.Dto;

namespace MCVTask.Domain.Department.Dto
{
    public class DepartmentDto : BaseDto<int>
    {
        #region Properties
        public string Name { get; set; }
        #endregion
    }
}
