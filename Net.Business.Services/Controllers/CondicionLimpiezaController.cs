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
    public class CondicionLimpiezaController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public CondicionLimpiezaController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Obtener lista de Condicion Limpieza
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de mantenimiento registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindCondicionLimpieza>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery]DtoFindCondicionLimpieza value)
        {

            var objectGetAll = await _repository.CondicionLimpieza.GetAll(value.CondicionLimpieza());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        /// <summary>
        /// Obtener un Condicion Limpieza individual
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetByIdCondicionLimpieza")]
        [ProducesResponseType(200, Type = typeof(List<DtoFindCondicionLimpieza>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetbyId(int id)
        {
            var objectGetById = await _repository.CondicionLimpieza.GetById(new DtoFindCondicionLimpieza { IdCondicionLimpieza = id }.CondicionLimpieza());

            if (objectGetById == null)
            {
                return NotFound();
            }

            return Ok(objectGetById);
        }

        /// <summary>
        /// Crear un nuevo Condicion Limpieza
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Id del registro creado</returns>
        /// <response code="201">Devuelve el elemento recién creado</response>
        /// <response code="400">Si el objeto enviado es nulo o invalido</response>  
        /// <response code="500">Algo salio mal guardando el registro</response>  
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(DtoInsertMantenimiento))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DtoInsertCondicionLimpieza value)
        {
            if (value == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            int ObjectNew = await _repository.CondicionLimpieza.Create(value.CondicionLimpieza());

            if (ObjectNew == 0)
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro {value.Descripcion}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetByIdCondicionLimpieza", new { id = ObjectNew }, ObjectNew);
        }

        /// <summary>
        /// Actualizar un mantenimiento existente
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="204">Actualizado Satisfactoriamente</response>
        /// <response code="404">Si el objeto enviado es nulo o invalido</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] DtoUpdateCondicionLimpieza value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.CondicionLimpieza.Update(value.CondicionLimpieza());

            return NoContent();
        }

        /// <summary>
        /// Eliminar registro de mantenimiento
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
        public async Task<IActionResult> Delete([FromBody] DtoDeleteCondicionLimpieza value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.CondicionLimpieza.Delete(value.CondicionLimpieza());

            return NoContent();
        }
    }
}