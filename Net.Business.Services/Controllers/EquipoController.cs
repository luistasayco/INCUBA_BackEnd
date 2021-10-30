using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Business.DTO;
using Net.Data;
using Newtonsoft.Json;

namespace Net.Business.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "ApiMaestroGeneral")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EquipoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public EquipoController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Obtener lista de equipo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de mantenimiento registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindEquipo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery]DtoFindGeneral value)
        {
            var objectGetAll = await _repository.Equipo.GetAll(value.General());
            return Ok(objectGetAll);
        }

        /// <summary>
        /// Obtener lista de equipo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de mantenimiento registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindEquipo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllPorFiltros([FromQuery] DtoFindGeneral value)
        {
            var objectGetAll = await _repository.Equipo.GetAllPorFiltros(value.General());
            return Ok(objectGetAll);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllXmlPorFiltros([FromQuery] string listaEmpresaPlanta, string listCodigoModelo)
        {
            DtoFindMasivoGeneral value = new DtoFindMasivoGeneral
            {
                ListaEmpresaPlanta = JsonConvert.DeserializeObject<List<DtoEmpresaPlanta>>(listaEmpresaPlanta),
                ListCodigoModelo = JsonConvert.DeserializeObject<List<DtoModelo>>(listCodigoModelo),
            };

            var objectGetAll = await _repository.Equipo.GetAllXmlPorFiltros(value.General());
            return Ok(objectGetAll);
        }
    }
}