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
    [ApiExplorerSettings(GroupName = "ApiMaestroGeneral")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ModeloController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public ModeloController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Obtener lista de modelo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de mantenimiento registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindModelo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery]DtoFindModelo value)
        {

            var objectGetAll = await _repository.Modelo.GetAll(value.Modelo());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }
    }
}