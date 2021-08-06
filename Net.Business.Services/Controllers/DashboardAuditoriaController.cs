using Net.Business.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Net.Data;

namespace Net.Business.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "ApiDashboardAuditoria")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DashboardAuditoriaController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public DashboardAuditoriaController(IRepositoryWrapper repository) 
        {
            this._repository = repository;
        }

        /// <summary>
        /// Metodo que obtiene los datos de auditoria filtrados
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindDashboardAuditoria value) 
        {
            var objectGetAll = await _repository.DashboardAuditoria.GetAll(value.DashboardAuditoriaPorFiltro());
            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

    }
}
