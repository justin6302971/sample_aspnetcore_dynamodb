using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SP.WebApi.Domain.DTO;
using SP.WebApi.Services.Interfaces;
using SP.WepApi.Domain.Models.AWS;

namespace SP.WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger
                                 , IEmployeeService employeeService
        )
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [Route("{LoginName}")]
        [HttpGet]
        public async Task<EmployeeDTO> Get(string LoginName)
        {
            return await _employeeService.GetByIDAsync(LoginName);
        }

        [Route("GetByName/{FirstName}")]
        [HttpGet]
        public async Task<List<EmployeeDTO>> GetByName(string FirstName, [FromQuery] string LastName)
        {

            var result = await _employeeService.GetByName(FirstName, LastName);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateDTO model, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeDTO result = default(EmployeeDTO);

            try
            {
                result = await _employeeService.CreateAsync(model);

            }
            catch (ArgumentException aex)
            {

                ModelState.AddModelError("LoginAlias", aex.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                //todo:log issues
                throw ex;
            }

            //todo: general service response
            return Ok(result);

        }


        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdateDTO model, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeDTO result = default(EmployeeDTO);

            try
            {
                result = await _employeeService.UpdateAsync(model);

            }
            catch (ArgumentException aex)
            {

                ModelState.AddModelError("LoginAlias", aex.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                //todo:log issues
                throw ex;
            }

            //todo: general service response
            return Ok(result);
        }

        [Route("{LoginName}")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteEmployee(string LoginName)
        {
            var result = await _employeeService.DeleteAsync(LoginName);
            return Ok(result);
        }
    }
}
