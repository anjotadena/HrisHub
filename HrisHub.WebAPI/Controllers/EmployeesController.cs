using AutoMapper;
using HrisHub.Dal;
using HrisHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HrisHub.WebAPI.DTO;

namespace HrisHub.WebAPI.Controllers
{
    public class EmployeesController : APIController
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
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Get()
        {
            var employees = await _employeeRepository.GetAll();

            return employees.Count <= 0 ? NotFound() : Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(employees));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,HR")]
        public async Task<ActionResult<EmployeeDTO>> GetDetails(int id)
        {
            var employee = await _employeeRepository.GetDetails(id);
               
            return employee == null ? NotFound() : Ok(_mapper.Map<EmployeeDTO>(employee));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "HR")]
        public async Task<ActionResult<Employee>> Create(NewEmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);

            var result = await _employeeRepository.Insert(employee);

            if (result == null)
            {
                return BadRequest();
            }

            var employeeDetails = _mapper.Map<EmployeeDTO>(employee);

            return CreatedAtAction("GetDetails", new { id = employeeDetails.Id }, employeeDetails);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "HR")]
        public async Task<ActionResult<Employee>> Update(int id, UpdateEmployeeDTO employeeDTO)
        {
            var currentEmployee = await _employeeRepository.GetDetails(id);

            if (currentEmployee == null)
            {
                return NotFound();
            }

            var employee = _mapper.Map<Employee>(employeeDTO);
            var result = await _employeeRepository.Update(id, employee);

            if (result == null)
            {
                return BadRequest();
            } 

            var employeeDetails = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employeeDetails);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "HR")]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = _employeeRepository.GetDetails(id);

            if (employee == null)
            {
                return NotFound();
            }

            var result = await _employeeRepository.Delete(id);

            if (result == null)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
