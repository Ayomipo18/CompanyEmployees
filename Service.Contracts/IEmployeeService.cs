using Shared.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges);
        Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);

        Task<EmployeeDto> CreateEmployeeAsync(Guid companyId, EmployeeCreateDto employeeForCreation, bool trackChanges);
        Task DeleteEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        Task UpdateEmployeeAsync(Guid companyId, Guid id, EmployeeUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);
        Task<(EmployeeUpdateDto employeePatch, Employee employee)> GetEmployeePatchAsync(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges);
        Task SavePatchChangesAsync(EmployeeUpdateDto employeePatch, Employee employee);
    }
}
