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
    [ApiExplorerSettings(GroupName = "ApiDashboardMantenimiento")]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class DashboardMantenimientoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public DashboardMantenimientoController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Médoto que obtiene el conteo de cdocumento por perido
        /// </summary>
        /// <param name="value">Recibe los paramtros de filtro</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindDashboardMantenimiento value)
        {
            var objectGetAll = await _repository.DashboardMantenimiento.GetAll(value.DashboardMantenimientoPorFiltro());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }
    }
}
