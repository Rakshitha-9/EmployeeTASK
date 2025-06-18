using Microsoft.ILP2025.EmployeeCRUD.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const string FilePath = "employees.json";
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(ILogger<EmployeeRepository> logger)
        {
            _logger = logger;
        }

        public async Task<List<EmployeeEntity>> GetAllEmployees()
        {
            _logger.LogInformation("Fetching all employees from repository.");
            return await Task.FromResult(GetEmployees());
        }

        // 🔧 Renamed to match expected interface
        public async Task<EmployeeEntity?> GetEmployeeById(int id)
        {
            _logger.LogInformation($"Fetching employee with ID = {id}");
            var employees = GetEmployees();
            return await Task.FromResult(employees.FirstOrDefault(e => e.Id == id));
        }

        // 🔧 Renamed to match expected interface
        public async Task AddEmployee(EmployeeEntity employee)
        {
            _logger.LogInformation("Adding a new employee.");
            var employees = GetEmployees();
            employee.Id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
            employees.Add(employee);
            await SaveEmployees(employees);
            _logger.LogInformation($"Employee with ID = {employee.Id} added.");
        }

        public async Task UpdateEmployee(EmployeeEntity updatedEmployee)
        {
            _logger.LogInformation($"Updating employee with ID = {updatedEmployee.Id}");
            var employees = GetEmployees();
            var employee = employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);
            if (employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;
                employee.PhoneNumber = updatedEmployee.PhoneNumber;
                employee.Salary = updatedEmployee.Salary;
                employee.Department = updatedEmployee.Department;
                employee.Location = updatedEmployee.Location;
                await SaveEmployees(employees);
                _logger.LogInformation($"Employee with ID = {updatedEmployee.Id} updated.");
            }
            else
            {
                _logger.LogWarning($"Employee with ID = {updatedEmployee.Id} not found for update.");
            }
        }

        public async Task DeleteEmployee(int id)
        {
            _logger.LogInformation($"Deleting employee with ID = {id}");
            var employees = GetEmployees();
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
                await SaveEmployees(employees);
                _logger.LogInformation($"Employee with ID = {id} deleted.");
            }
            else
            {
                _logger.LogWarning($"Employee with ID = {id} not found for deletion.");
            }
        }

        private List<EmployeeEntity> GetEmployees()
        {
            if (!File.Exists(FilePath))
                return new List<EmployeeEntity>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<EmployeeEntity>>(json) ?? new List<EmployeeEntity>();
        }

        private async Task SaveEmployees(List<EmployeeEntity> employees)
        {
            var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }
    }
}
