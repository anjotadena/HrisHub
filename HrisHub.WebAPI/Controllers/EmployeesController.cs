using HrisHub.Dal;
using HrisHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HrisHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ICommonRepository<Employee> _employeeRepository;

        public EmployeesController(ICommonRepository<Employee> repository)
        {
            _employeeRepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            var employees = _employeeRepository.GetAll();

            return employees.Count <= 0 ? NotFound() : Ok(employees);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,HR")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _employeeRepository.GetDetails(id);
               
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "HR")]
        public ActionResult<Employee> Create(Employee employee)
        {
            _employeeRepository.Insert(employee);

            var result = _employeeRepository.SaveChanges();

            return result > 0 ? CreatedAtAction("GetDetails", new { id = employee.Id }, employee) : BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Employee,HR")]
        public ActionResult<Employee> Update(Employee employee)
        {
            _employeeRepository.Update(employee);

            var result = _employeeRepository.SaveChanges();

            return result > 0 ? NoContent() : BadRequest();
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
