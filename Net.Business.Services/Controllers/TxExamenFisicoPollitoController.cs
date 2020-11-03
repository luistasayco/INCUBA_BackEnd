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
    [ApiExplorerSettings(GroupName = "ApiExamenFisicoPollito")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TxExamenFisicoPollitoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public TxExamenFisicoPollitoController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Obtener lista de registro de equipo
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de mantenimiento registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DtoFindTxExamenFisicoPollitoPorFiltro>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindTxExamenFisicoPollitoPorFiltro value)
        {

            var objectGetAll = await _repository.TxExamenFisicoPollito.GetAll(value.RetornaTxExamenFisicoPollito());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            //DtoResponseListTxExamenFisicoPollito data = new DtoResponseListTxExamenFisicoPollito().RetornarTxExamenFisicoPollitoResponse(objectGetAll);

            return Ok(objectGetAll);
        }

        /// <summary>
        /// Obtener un TxRegistro Equipo
        /// </summary>
        /// <param name="id">Id</param>g
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetByIdTxExamenFisicoPollito")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByIdTxExamenFisicoPollito(int id)
        {
            var objectGetById = await _repository.TxExamenFisicoPollito.GetById(new DtoFindTxExamenFisicoPollitoPorId { IdExamenFisico = id }.RetornaTxExamenFisicoPollito());

            if (objectGetById == null)
            {
                return NotFound();
            }

            return Ok(objectGetById);
        }

        /// <summary>
        /// Obtener un TxRegistro Equipo
        /// </summary>
        /// <param>Id</param>g
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet(Name = "GetByIdTxExamenFisicoPollitoNew")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByIdTxExamenFisicoPollitoNew()
        {
            var objectGetById = await _repository.TxExamenFisicoPollito.GetByIdNew(new DtoFindTxExamenFisicoPollitoPorId { IdExamenFisico = 0 }.RetornaTxExamenFisicoPollito());

           if (objectGetById == null)
            {
                return NotFound();
            }

            return Ok(objectGetById);
        }

        /// <summary>
        /// Crear un informe de registro de equipo
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Id del registro creado</returns>
        /// <response code="201">Devuelve el elemento recién creado</response>
        /// <response code="400">Si el objeto enviado es nulo o invalido</response>  
        /// <response code="500">Algo salio mal guardando el registro</response>  
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(DtoInsertTxExamenFisicoPollito))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DtoInsertTxExamenFisicoPollito value)
        {
            if (value == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            int ObjectNew = await _repository.TxExamenFisicoPollito.Create(value.RetornaTxExamenFisicoPollito());

            if (ObjectNew == 0)
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetByIdTxExamenFisicoPollito", new { id = ObjectNew }, ObjectNew);
        }
        /// <summary>
        /// Actualizar una calidad existente
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="204">Actualizado Satisfactoriamente</response>
        /// <response code="404">Si el objeto enviado es nulo o invalido</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] DtoUpdateTxExamenFisicoPollito value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxExamenFisicoPollito.Update(value.RetornaTxExamenFisicoPollito());

            return NoContent();
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
        public async Task<IActionResult> Delete([FromBody] DtoDeleteTxExamenFisicoPollito value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxExamenFisicoPollito.Delete(value.RetornaTxExamenFisicoPollito());

            return NoContent();
        }

        /// <summary>
        /// Obtener un TxRegistro Equipo
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetGeneraPdfByIdTxExamenFisicoPollito")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<FileContentResult> GetGeneraPdfByIdTxExamenFisicoPollito(int id)
        {
            var objectGetById = await _repository.TxExamenFisicoPollito.GenerarPDF(new DtoFindTxExamenFisicoPollitoPorId { IdExamenFisico = id }.RetornaTxExamenFisicoPollito());

            var pdf = File(objectGetById.GetBuffer(), "applicacion/pdf", "diploma.pdf");

            return pdf;
        }
    }
}
