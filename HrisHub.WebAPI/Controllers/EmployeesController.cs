using AutoMapper;
using HrisHub.Dal;
using HrisHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HrisHub.WebAPI.DTO;

namespace HrisHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ICommonRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(ICommonRepository<Employee> repository, IMapper mapper)
        {
            _employeeRepository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,HR")]
        public ActionResult<IEnumerable<EmployeeDTO>> Get()
        {
            var employees = _employeeRepository.GetAll();

            return employees.Count <= 0 ? NotFound() : Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(employees));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,HR")]
        public ActionResult<EmployeeDTO> GetDetails(int id)
        {
            var employee = _employeeRepository.GetDetails(id);
               
            return employee == null ? NotFound() : Ok(_mapper.Map<EmployeeDTO>(employee));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "HR")]
        public ActionResult<Employee> Create(NewEmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);

            _employeeRepository.Insert(employee);

            var result = _employeeRepository.SaveChanges();
            var employeeDetails = _mapper.Map<EmployeeDTO>(employee);

            return result > 0 ? CreatedAtAction("GetDetails", new { id = employeeDetails.Id }, employeeDetails) : BadRequest();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "HR")]
        public ActionResult<Employee> Update(UpdateEmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            _employeeRepository.Update(employee);

            var result = _employeeRepository.SaveChanges();

            var employeeDetails = _mapper.Map<EmployeeDTO>(employee);

            return result > 0 ? Ok(employeeDetails) : BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "HR")]
        public ActionResult<Employee> Delete(int id)
        {
            var employee = _employeeRepository.GetDetails(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeRepository.Delete(employee);
            _employeeRepository.SaveChanges();

            return NoContent();
        }
    }
}
