using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper; 
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employees = _repository.Employee.GetEmployees(companyId, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employee = _repository.Employee.GetEmployee(companyId, id, trackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(id);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public EmployeeDto CreateEmployee(Guid companyId, EmployeeCreateDto employeeForCreation, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null) 
                throw new CompanyNotFoundException(companyId);

            var employee = _mapper.Map<Employee>(employeeForCreation);

            _repository.Employee.CreateEmployee(companyId, employee);
            _repository.Save();

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public void DeleteEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if(company is null)
                throw new CompanyNotFoundException(companyId);

            var employee = _repository.Employee.GetEmployee(companyId, id, trackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(id);

            _repository.Employee.DeleteEmployee(employee);
            _repository.Save();
        }

        public void UpdateEmployee(Guid companyId, Guid id, EmployeeUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, compTrackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employee = _repository.Employee.GetEmployee(companyId, id, empTrackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdate, employee);
            _repository.Save();
        }

        public (EmployeeUpdateDto employeePatch, Employee employee) GetEmployeePatch (Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, compTrackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employee = _repository.Employee.GetEmployee(companyId, id, empTrackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(id);

            var employeePatch = _mapper.Map<EmployeeUpdateDto>(employee);

            return (employeePatch, employee);
        }

        public void SavePatchChanges(EmployeeUpdateDto employeePatch, Employee employee)
        {
            _mapper.Map(employeePatch, employee);
            _repository.Save();
        }
    }
}
