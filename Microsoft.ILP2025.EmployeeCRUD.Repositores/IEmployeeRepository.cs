using Microsoft.ILP2025.EmployeeCRUD.Entities;

namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeEntity>> GetAllEmployees();
        Task<EmployeeEntity?> GetEmployeeById(int id); // matches the service
        Task AddEmployee(EmployeeEntity employee);     // matches the service
        Task UpdateEmployee(EmployeeEntity employee);
        Task DeleteEmployee(int id);
    }
}
