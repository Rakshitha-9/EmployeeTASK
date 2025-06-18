using Microsoft.ILP2025.EmployeeCRUD.Entities;
using Microsoft.ILP2025.EmployeeCRUD.Repositores; // <-- for IEmployeeRepository
using Microsoft.ILP2025.EmployeeCRUD.Servcies;   // <-- for IEmployeeService

namespace Microsoft.ILP2025.EmployeeCRUD.Servcies
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllEmployees()
        {
            return await _repository.GetAllEmployees();
        }

        public async Task<EmployeeEntity> GetEmployeeById(int id)
        {
            return await _repository.GetEmployeeById(id);
        }

        public async Task AddEmployee(EmployeeEntity employee)
        {
            await _repository.AddEmployee(employee);
        }

        public async Task UpdateEmployee(EmployeeEntity employee)
        {
            await _repository.UpdateEmployee(employee);
        }

        public async Task DeleteEmployee(int id)
        {
            await _repository.DeleteEmployee(id);
        }
    }
}
