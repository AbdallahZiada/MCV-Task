using FluentValidation;

namespace MCVTask.AppService.AppService.Employee.DeleteEmployee
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
