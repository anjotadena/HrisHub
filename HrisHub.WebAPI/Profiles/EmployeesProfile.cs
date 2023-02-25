using AutoMapper;
using HrisHub.Models;
using HrisHub.WebAPI.DTO;

namespace HrisHub.WebAPI.Profiles
{
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<NewEmployeeDTO, Employee>();
            CreateMap<UpdateEmployeeDTO, Employee>();
        }
    }
}
