using MCVTask.Base.Entity;

namespace MCVTask.Domain.Department.Entity
{
    public class Department : Entity<int>, IAggregateRoot
    {
        #region Properties
        public string Name { get; set; }
        public ICollection<Employee.Entity.Employee> Employees { get; set; }
        #endregion

        #region Methods
        public void CreateDepartment(string name)
        {
            Name = name;
        }
        public void UpdateDepartment(string name)
        {
            Name = name;
        }
        public void DeleteDepartment()
        {
            IsDeleted = true;
        }
        #endregion
    }
}
