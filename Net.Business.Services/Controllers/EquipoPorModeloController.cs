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
    [ApiExplorerSettings(GroupName = "ApiMaestroRegistroEquipo")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EquipoPorModeloController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public EquipoPorModeloController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Obtener lista de equipos por seleccionar
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista de equipos faltantes por seleccionar</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindEquipoPorModelo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllSeleccionado([FromQuery]DtoFindEquipoPorModelo value)
        {

            var objectGetAll = await _repository.EquipoPorModelo.GetAllSeleccionado(value.EquipoPorModelo());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }
    }
}