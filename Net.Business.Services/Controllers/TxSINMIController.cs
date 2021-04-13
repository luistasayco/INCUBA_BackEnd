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
    [ApiExplorerSettings(GroupName = "ApiSINMI")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TxSINMIController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public TxSINMIController(IRepositoryWrapper repository)
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindTxSINMI value)
        {

            var objectGetAll = await _repository.TxSINMI.GetAll(value.RetornaTxSINMI());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllDetalleNew()
        {

            var objectGetAll = await _repository.TxSINMI.GetAllDetalleNew();

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByCodigoEmpresa([FromQuery] string codigoEmpresa)
        {

            var objectGetAll = await _repository.TxSINMI.GetByCodigoEmpresa(codigoEmpresa);

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
        [HttpGet("{id}", Name = "GetByIdTxSINMI")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByIdTxSINMI(int id)
        {
            var objectGetById = await _repository.TxSINMI.GetById(new DtoFindTxSINMIPorId { IdSINMI = id }.RetornaTxSINMI());

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DtoInsertTxSINMI value)
        {
            if (value == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            int ObjectNew = await _repository.TxSINMI.Create(value.RetornaTxSINMI());

            if (ObjectNew == 0)
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro");
                return StatusCode(500, ModelState);
            }
            else
            {
                if (value.FlgCerrado != null)
                {
                    if (bool.Parse(value.FlgCerrado.ToString()))
                    {
                        var updateStatus = new DtoUpdateStatusTxSINMI { IdSINMI = ObjectNew, IdUsuarioCierre = int.Parse(value.RegUsuario.ToString()), RegUsuario = value.RegUsuario, RegEstacion = value.RegEstacion };
                        var result = await _repository.TxSINMI.UpdateStatus(updateStatus.RetornaTxSINMI());

                        if (result.ResultadoCodigo == -1)
                        {
                            return BadRequest(result);
                        }
                    }
                }

            }

            return CreatedAtRoute("GetByIdTxSINMI", new { id = ObjectNew }, ObjectNew);
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
        public async Task<IActionResult> Update([FromBody] DtoUpdateTxSINMI value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSINMI.Update(value.RetornaTxSINMI());

            return NoContent();
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
        public async Task<IActionResult> UpdateStatus([FromBody] DtoUpdateStatusTxSINMI value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.TxSINMI.UpdateStatus(value.RetornaTxSINMI());

            if (result.ResultadoCodigo == -1)
            {
                return BadRequest(result);
            }

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
        public async Task<IActionResult> Delete([FromBody] DtoDeleteTxSINMI value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSINMI.Delete(value.RetornaTxSINMI());

            return NoContent();
        }

        /// <summary>
        /// Obtener un TxRegistro Equipo
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetGeneraPdfByIdTxSINMI")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<FileContentResult> GetGeneraPdfByIdTxSINMI(int id)
        {
            var objectGetById = await _repository.TxSINMI.GenerarPDF(new DtoFindTxSINMIPorId { IdSINMI = id }.RetornaTxSINMI());

            var pdf = File(objectGetById.GetBuffer(), "applicacion/pdf", "diploma.pdf");

            return pdf;
        }
    }
}
