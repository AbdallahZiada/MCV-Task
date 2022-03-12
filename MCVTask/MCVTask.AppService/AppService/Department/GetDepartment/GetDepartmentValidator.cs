using FluentValidation;

namespace MCVTask.AppService.AppService.Department.GetDepartment
{
    public class GetDepartmentValidator : AbstractValidator<GetDepartmentCommand>
    {
        public GetDepartmentValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
