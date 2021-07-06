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
    public class TxSINMIConsolidadoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public TxSINMIConsolidadoController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindTxSINMIConsolidado value)
        {

            var objectGetAll = await _repository.TxSINMIConsolidado.GetAll(value.RetornaTxSINMIConsolidado());

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
        [HttpGet("{id}", Name = "GetByIdTxSINMIConsolidado")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByIdTxSINMIConsolidado(int id)
        {
            var objectGetById = await _repository.TxSINMIConsolidado.GetById(new DtoFindTxSINMIConsolidadoPorId { IdSINMIConsolidado = id }.RetornaTxSINMIConsolidado());

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
        public async Task<IActionResult> Create([FromBody] DtoInsertTxSINMIConsolidado value)
        {
            if (value == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var ObjectNew = await _repository.TxSINMIConsolidado.Create(value.RetornaTxSINMIConsolidado());

            if (ObjectNew.ResultadoCodigo == -1)
            {
                return BadRequest(ObjectNew);
            }

            return Ok(ObjectNew);
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
        public async Task<IActionResult> Update([FromBody] DtoUpdateTxSINMIConsolidado value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSINMIConsolidado.Update(value.RetornaTxSINMIConsolidado());

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus([FromBody] DtoUpdateStatusTxSINMIConsolidado value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSINMIConsolidado.UpdateStatus(value.RetornaTxSINMIConsolidado());

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
        public async Task<IActionResult> Delete([FromBody] DtoDeleteTxSINMIConsolidado value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSINMIConsolidado.Delete(value.RetornaTxSINMIConsolidado());

            return NoContent();
        }


        [HttpGet(Name = "GetGeneraPdfByIdTxSINMIConsolidado")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<FileContentResult> GetGeneraPdfByIdTxSINMIConsolidado([FromQuery] int id, string descripcionEmpresa)
        {
            var objectGetById = await _repository.TxSINMIConsolidado.GenerarPDF(id, descripcionEmpresa);

            var pdf = File(objectGetById.GetBuffer(), "applicacion/pdf", "diploma.pdf");

            return pdf;
        }
    }
}
