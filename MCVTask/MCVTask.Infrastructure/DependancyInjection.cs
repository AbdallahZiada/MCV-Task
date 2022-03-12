using MCVTask.Domain.Department.Repository;
using MCVTask.Domain.Employee.Repository;
using MCVTask.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MCVTask.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) 
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
