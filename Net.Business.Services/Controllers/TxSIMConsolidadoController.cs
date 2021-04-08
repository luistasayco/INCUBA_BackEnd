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
    [ApiExplorerSettings(GroupName = "ApiSIMConsolidado")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TxSIMConsolidadoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public TxSIMConsolidadoController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindTxSIMConsolidado value)
        {

            var objectGetAll = await _repository.TxSIMConsolidado.GetAll(value.RetornaTxSIMConsolidado());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        /// <summary>
        /// Obtener un TxRegistro Equipo
        /// </summary>
        /// <param name="id">Id</param>g
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetByIdTxSIMConsolidado")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByIdTxSIMConsolidado(int id)
        {
            var objectGetById = await _repository.TxSIMConsolidado.GetById(new DtoFindTxSIMConsolidadoPorId { IdSIMConsolidado = id }.RetornaTxSIMConsolidado());

            if (objectGetById == null)
            {
                return NotFound();
            }

            return Ok(objectGetById);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DtoInsertTxSIMConsolidado value)
        {
            if (value == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            int ObjectNew = await _repository.TxSIMConsolidado.Create(value.RetornaTxSIMConsolidado());

            if (ObjectNew == 0)
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetByIdTxSIMConsolidado", new { id = ObjectNew }, ObjectNew);
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
        public async Task<IActionResult> Update([FromBody] DtoUpdateTxSIMConsolidado value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSIMConsolidado.Update(value.RetornaTxSIMConsolidado());

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus([FromBody] DtoUpdateStatusTxSIMConsolidado value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSIMConsolidado.UpdateStatus(value.RetornaTxSIMConsolidado());

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
        public async Task<IActionResult> Delete([FromBody] DtoDeleteTxSIMConsolidado value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSIMConsolidado.Delete(value.RetornaTxSIMConsolidado());

            return NoContent();
        }

       
        [HttpGet(Name = "GetGeneraPdfByIdTxSIMConsolidado")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<FileContentResult> GetGeneraPdfByIdTxSIMConsolidado([FromQuery] int id, string descripcionEmpresa)
        {
            var objectGetById = await _repository.TxSIMConsolidado.GenerarPDF(id , descripcionEmpresa);

            var pdf = File(objectGetById.GetBuffer(), "applicacion/pdf", "diploma.pdf");

            return pdf;
        }
    }
}
