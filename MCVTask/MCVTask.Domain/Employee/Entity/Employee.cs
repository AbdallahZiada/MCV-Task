using MCVTask.Base.Entity;

namespace MCVTask.Domain.Employee.Entity
{
    public class Employee : Entity<int>, IAggregateRoot
    {
        #region Properties
        public string EmployeeId { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string JobTitle { get; private set; }
        public DateTime HiringDate { get; private set; }
        public Department.Entity.Department Department { get; private set; }
        #endregion

        #region Methods
        public void CreateEmployee(string employeeId, string name, string phoneNumber, DateTime birthDate, string title, DateTime hiringDate, Department.Entity.Department department)
        {
            EmployeeId = employeeId;
            Name = name;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            JobTitle = title;
            HiringDate = hiringDate;
            Department = department;
        }
        public void UpdateEmployee(string employeeId, string name, string phoneNumber, DateTime birthDate, string title, DateTime hiringDate, Department.Entity.Department department)
        {
            EmployeeId = employeeId;
            Name = name;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            JobTitle = title;
            HiringDate = hiringDate;
            Department = department;
        }
        public void DeleteEmployee()
        {
            IsDeleted = true;
        }
            #endregion
    }
}
