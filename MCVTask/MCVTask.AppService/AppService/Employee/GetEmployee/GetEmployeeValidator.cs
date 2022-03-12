using FluentValidation;

namespace MCVTask.AppService.AppService.Employee.GetEmployee
{
    public class GetEmployeeValidator : AbstractValidator<GetEmployeeCommand>
    {
        public GetEmployeeValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
