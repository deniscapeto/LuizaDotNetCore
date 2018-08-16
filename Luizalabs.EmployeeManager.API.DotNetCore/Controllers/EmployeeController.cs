using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luizalabs.EmployeeManager.API.DotNetCore.Data;
using Luizalabs.EmployeeManager.API.DotNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Luizalabs.EmployeeManager.API.DotNetCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get(int page_size, int page)
        {
            try
            {
                var employees = _repository.List(page_size, page);
                return new JsonResult(employees);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try
            {
                _repository.Add(employee);
                return Created("", employee);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _repository.Delete(id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}