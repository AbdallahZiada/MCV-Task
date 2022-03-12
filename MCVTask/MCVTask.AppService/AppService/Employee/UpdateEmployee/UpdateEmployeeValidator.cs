using FluentValidation;
using MCVTask.Domain.Employee.Repository;

namespace MCVTask.AppService.AppService.Employee.UpdateEmployee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Title).NotEmpty().MinimumLength(2);
            RuleFor(command => command.BirthDate).LessThan(DateTime.Now);
            RuleFor(command => command.DepartmentId).GreaterThan(0);
            RuleFor(command => command).Must(IsUniqueEmployeeId).WithMessage("Employee Id already taken");
        }

        private bool IsUniqueEmployeeId(UpdateEmployeeCommand request)
        {
            return (_employeeRepository.IsUniqueEmployeeId(request.EmployeeId, request.Id).Result);
        }
    }
}
