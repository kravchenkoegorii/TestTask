using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestTask.API.Exceptions;
using TestTask.Domain.Models;
using TestTask.Persistence.Services;

namespace TestTask.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Return collection of students with professors using EF
        /// </summary>
        [HttpGet("get_by_ef")]
        [SwaggerOperation(
            Summary = "Return collection of students with professors using EF.",
            Description = "Return collection of students with professors using EF.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
        public async Task<ICollection<Professor>> GetUsingEF()
        {
            return await _dataService.GetAllUsingEF();
        }

        /// <summary>
        /// Return collection of students with professors using Dapper
        /// </summary>
        [HttpGet("get_by_dapper")]
        [SwaggerOperation(
            Summary = "Return collection of students with professors using Dapper.",
            Description = "Return collection of students with professors using Dapper.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
        public async Task<ICollection<Student>> GetUsingDapper()
        {
            return await _dataService.GetAllUsingDapper();
        }
    }
}
