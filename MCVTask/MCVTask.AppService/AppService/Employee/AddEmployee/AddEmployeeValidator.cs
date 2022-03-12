using FluentValidation;
using MCVTask.Domain.Employee.Repository;

namespace MCVTask.AppService.AppService.Employee.AddEmployee
{
    public class AddEmployeeValidator : AbstractValidator<AddEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public AddEmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            RuleFor(command => command.EmployeeId).NotEmpty();
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Title).NotEmpty().MinimumLength(2);
            RuleFor(command => command.BirthDate).LessThan(DateTime.Now);
            RuleFor(command => command.DepartmentId).GreaterThan(0);
            RuleFor(command => command).Must(IsUniqueEmployeeId).WithMessage("Employee Id already taken");
        }

        private bool IsUniqueEmployeeId(AddEmployeeCommand request)
        {
            return (_employeeRepository.IsUniqueEmployeeId(request.EmployeeId).Result);
        }
    }
}
