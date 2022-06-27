using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Service.Contracts;
using Entities.Models;
using Shared.DataTransferObjects;
using AutoMapper;
using Entities.Exceptions;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;   
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
        }

        public CompanyDto GetCompany(Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(id, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(id);
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }

        public CompanyDto CreateCompany(CompanyCreateDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();
            var companyDto = _mapper.Map<CompanyDto>(companyEntity);
            return companyDto;
        }

        public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if(ids is null)
                throw new IdParametersBadRequestException();
            var companies = _repository.Company.GetByIds(ids, trackChanges);
            if (ids.Count() != companies.Count())
                throw new CollectionByIdsBadRequestException();
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
        }

        public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection (IEnumerable<CompanyCreateDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();

            var companies = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var company in companies)
            {
                _repository.Company.CreateCompany(company);
            }
            _repository.Save();
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            var ids = string.Join(",", companiesDto.Select(c => c.Id));
            return (companies: companiesDto, ids: ids);
        }
    }
}
