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
    [ApiExplorerSettings(GroupName = "ApiMaestroExamenPollito")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProcesoDetalleController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public ProcesoDetalleController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Obtener lista de proceso
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de Calidad registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindProcesoDetalle>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery]DtoFindProcesoDetalle value)
        {

            var objectGetAll = await _repository.ProcesoDetalle.GetAll(value.ProcesoDetalle());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }
        /// <summary>
        /// Obtener una proceso individual
        /// </summary>
        /// <param name="id">Id de Calidad</param>
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetByIdProcesoDetalle")]
        [ProducesResponseType(200, Type = typeof(List<DtoFindProcesoDetalle>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetbyId(int id)
        {
            var objectGetById = await _repository.ProcesoDetalle.GetById(new DtoFindProcesoDetalle { IdProceso = id }.ProcesoDetalle());

            if (objectGetById == null)
            {
                return NotFound();
            }

            return Ok(objectGetById);
        }

        /// <summary>
        /// Crear una nueva ProcesoDetalle
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
        public async Task<IActionResult> Create([FromBody] DtoInsertProcesoDetalle value)
        {
            if (value == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            int ObjectNew = await _repository.ProcesoDetalle.Create(value.ProcesoDetalle());

            if (ObjectNew == 0)
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro {value.DescripcionProcesoDetalle}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetByIdProcesoDetalle", new { id = ObjectNew }, ObjectNew);
        }

        /// <summary>
        /// Actualizar una ProcesoDetalle existente
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="204">Actualizado Satisfactoriamente</response>
        /// <response code="404">Si el objeto enviado es nulo o invalido</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] DtoUpdateProcesoDetalle value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.ProcesoDetalle.Update(value.ProcesoDetalle());

            return NoContent();
        }

        /// <summary>
        /// Eliminar logicamente una ProcesoDetalle
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
        public async Task<IActionResult> Delete([FromBody] DtoDeleteProcesoDetalle value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.ProcesoDetalle.Delete(value.ProcesoDetalle());

            return NoContent();
        }
    }
}