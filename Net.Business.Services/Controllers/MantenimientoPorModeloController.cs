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
    public class MantenimientoPorModeloController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public MantenimientoPorModeloController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Obtener lista de Mantenimiento Por Modelo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de mantenimiento registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindMantenimientoPorModelo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindMantenimientoPorModelo value)
        {
            var objectGetAll = await _repository.MantenimientoPorModelo.GetAll(value.MantenimientoPorModelo());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }
        /// <summary>
        /// Obtener lista de Mantenimiento Por Modelo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de mantenimiento registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindMantenimientoPorModelo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllSeleccionado([FromQuery]DtoFindMantenimientoPorModelo value)
        {
            var objectGetAll = await _repository.MantenimientoPorModelo.GetAllSeleccionado(value.MantenimientoPorModelo());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        /// <summary>
        /// Obtener lista de Mantenimiento Por Modelo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de mantenimiento registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindMantenimientoPorModelo>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllPorSeleccionar([FromQuery]DtoFindMantenimientoPorModelo value)
        {
            var objectGetAll = await _repository.MantenimientoPorModelo.GetAllPorSeleccionar(value.MantenimientoPorModelo());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        /// <summary>
        /// Crear un nuevo Mantenimiento Por Modelo
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Id del registro creado</returns>
        /// <response code="201">Devuelve el elemento recién creado</response>
        /// <response code="400">Si el objeto enviado es nulo o invalido</response>  
        /// <response code="500">Algo salio mal guardando el registro</response>  
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<DtoInsertMantenimientoPorModelo>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] List<DtoInsertMantenimientoPorModelo> value)
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
                ObjectNew = await _repository.MantenimientoPorModelo.Create(item.MantenimientoPorModelo());
            }

            return Ok();
        }

        /// <summary>
        /// Eliminar registro de Mantenimiento Por Modelo 
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
        public async Task<IActionResult> Delete([FromBody] List<DtoDeleteMantenimientoPorModelo> value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            foreach (var item in value)
            {
                await _repository.MantenimientoPorModelo.Delete(item.MantenimientoPorModelo());
            }

            return NoContent();
        }
    }
}