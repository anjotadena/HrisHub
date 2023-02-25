﻿using System.ComponentModel.DataAnnotations;

namespace HrisHub.WebAPI.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Skillsets { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;
    }
}
