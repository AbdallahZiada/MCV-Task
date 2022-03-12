using FluentValidation;

namespace MCVTask.AppService.AppService.Department.UpdateDepartment
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2);
        }
    }
}
