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
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
        EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges);

        EmployeeDto CreateEmployee(Guid companyId, EmployeeCreateDto employeeForCreation, bool trackChanges);
        void DeleteEmployee(Guid companyId, Guid id, bool trackChanges);
        void UpdateEmployee(Guid companyId, Guid id, EmployeeUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);
        (EmployeeUpdateDto employeePatch, Employee employee) GetEmployeePatch(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges);
        void SavePatchChanges(EmployeeUpdateDto employeePatch, Employee employee);
    }
}
