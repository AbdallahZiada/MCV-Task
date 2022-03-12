using FluentValidation;

namespace MCVTask.AppService.AppService.Department.AddDepartment
{
    public class AddDepartmentValidator : AbstractValidator<AddDepartmentCommand>
    {
        public AddDepartmentValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2);
        }
    }
}
