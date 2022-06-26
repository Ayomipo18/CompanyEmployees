﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record EmployeeDto {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public int Age { get; init; } 
        public string? Position { get; init; }
    }

    public record EmployeeCreateDto(string Name, int Age, string Position);
}
