using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Business.DTO;
using Net.Data;

namespace Net.Business.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "ApiDashboard")]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class DashboardController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public DashboardController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Médoto que obtiene los diferentes dashboards
        /// </summary>
        /// <param name="value">Recibe los paramtros de filtro</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindDashboard value)
        {
            var objectGetAll = await _repository.Dashboard.GetAll(value.DashboardPorCategoria());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }
    }
}