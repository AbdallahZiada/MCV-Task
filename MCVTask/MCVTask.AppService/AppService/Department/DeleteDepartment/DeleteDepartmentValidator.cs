using FluentValidation;
using MCVTask.Domain.Department.Repository;

namespace MCVTask.AppService.AppService.Department.DeleteDepartment
{
    public class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DeleteDepartmentValidator(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.Id).Must(HasNoEmployees).WithMessage("Can't delete department has employees in it").When(x => x.Id > 0);
        }

        private bool HasNoEmployees(int departmentId)
        {
            return (_departmentRepository.HasNoEmployees(departmentId).Result);
        }
    }
}
