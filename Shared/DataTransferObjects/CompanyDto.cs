﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CompanyDto {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? FullAddress { get; init; }
    }

    public record CompanyCreateDto(string Name, string Address, string Country, IEnumerable<EmployeeCreateDto> Employees);
    public record CompanyUpdateDto(string Name, string Address, string Country, IEnumerable<EmployeeCreateDto> Employees);
}
