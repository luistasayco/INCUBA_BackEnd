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
    [ApiExplorerSettings(GroupName = "ApiMaestroRegistroEquipo")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RepuestoPorModeloController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public RepuestoPorModeloController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// Obtener lista de Repuesto Por Modelo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de Calidad registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindRepuestoPorModelo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindRepuestoPorModelo value)
        {

            var objectGetAll = await _repository.RepuestoPorModelo.GetAll(value.RepuestoPorModelo());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        /// <summary>
        /// Obtener lista de Repuesto Por Modelo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de Calidad registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindRepuestoPorModelo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllSeleccionado([FromQuery]DtoFindRepuestoPorModelo value)
        {

            var objectGetAll = await _repository.RepuestoPorModelo.GetAllSeleccionado(value.RepuestoPorModelo());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetXmlSeleccionado([FromQuery] string value)
        {
            DtoXmlRepuestoPorModelo data = new DtoXmlRepuestoPorModelo
            {
                ListCodigoModelo = JsonConvert.DeserializeObject<List<DtoModelo>>(value)
            };

            var objectGetAll = await _repository.RepuestoPorModelo.GetXmlSeleccionado(data.General());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        /// <summary>
        /// Obtener lista de Repuesto Por Modelo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de Calidad registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindRepuestoPorModelo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllPorSeleccionar([FromQuery]DtoFindRepuestoPorModelo value)
        {

            var objectGetAll = await _repository.RepuestoPorModelo.GetAllPorSeleccionar(value.RepuestoPorModelo());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }


        /// <summary>
        /// Crear una nueva Repuesto Por Modelo
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Id del registro creado</returns>
        /// <response code="201">Devuelve el elemento recién creado</response>
        /// <response code="400">Si el objeto enviado es nulo o invalido</response>  
        /// <response code="500">Algo salio mal guardando el registro</response>  
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(DtoInsertCalidad))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] List<DtoInsertRepuestoPorModelo> value)
        {
            if (value == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            int ObjectNew = 0;

            foreach (var item in value)
            {
                ObjectNew = await _repository.RepuestoPorModelo.Create(item.RepuestoPorModelo());
            }

            return Ok();
        }

        /// <summary>
        /// Eliminar logicamente una Repuesto Por Modelo
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ///<response code="204">Eliminado Satisfactoriamente</response>
        ///<response code="400">Si el objeto enviado es nulo o invalido</response>
        ///<response code="409">Si ocurrio un conflicto</response>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete([FromBody] List<DtoDeleteRepuestoPorModelo> value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            foreach (var item in value)
            {
                await _repository.RepuestoPorModelo.Delete(item.RepuestoPorModelo());
            }

            return NoContent();
        }
    }
}