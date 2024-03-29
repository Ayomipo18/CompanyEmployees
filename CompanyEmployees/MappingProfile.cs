﻿using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployees
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress,
                    opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Employee, EmployeeDto>();

            CreateMap<CompanyCreateDto, Company>();

            CreateMap<EmployeeCreateDto, Employee>();

            CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();

            CreateMap<CompanyUpdateDto, Company>();

            CreateMap<EmployeeUpdateDto, Employee>();

            CreateMap<UserRegistrationDto, User>();
        }
    }
}
