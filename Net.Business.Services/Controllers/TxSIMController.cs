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
    [ApiExplorerSettings(GroupName = "ApiSIM")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TxSIMController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public TxSIMController(IRepositoryWrapper repository)
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
        public async Task<IActionResult> GetAll([FromQuery] DtoFindTxSim value)
        {

            var objectGetAll = await _repository.TxSIM.GetAll(value.RetornaTxSIM());

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

            var objectGetAll = await _repository.TxSIM.GetByCodigoEmpresa(codigoEmpresa);

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
        [HttpGet("{id}", Name = "GetByIdTxSIM")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByIdTxSIM(int id)
        {
            var objectGetById = await _repository.TxSIM.GetById(new DtoFindTxSimPorId { IdSIM = id }.RetornaTxSIM());

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
        //[HttpGet(Name = "GetByIdTxExamenFisicoPollitoNew")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> GetByIdTxExamenFisicoPollitoNew()
        //{
        //    var objectGetById = await _repository.TxExamenFisicoPollito.GetByIdNew(new DtoFindTxExamenFisicoPollitoPorId { IdExamenFisico = 0 }.RetornaTxExamenFisicoPollito());

        //    if (objectGetById == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(objectGetById);
        //}

        /// <summary>
        /// Crear un informe de registro de equipo
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Id del registro creado</returns>
        /// <response code="201">Devuelve el elemento recién creado</response>
        /// <response code="400">Si el objeto enviado es nulo o invalido</response>  
        /// <response code="500">Algo salio mal guardando el registro</response>  
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(DtoInsertTxSim))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DtoInsertTxSim value)
        {
            if (value == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var ObjectNew = await _repository.TxSIM.Create(value.RetornaTxSIM());

            if (ObjectNew.ResultadoCodigo == -1)
            {
                return BadRequest(ObjectNew);
            }
            else
            {
                if (value.FlgCerrado != null)
                {
                    if (bool.Parse(value.FlgCerrado.ToString()))
                    {
                        var updateStatus = new DtoUpdateStatusTxSim { IdSIM = ObjectNew.IdRegistro, IdUsuarioCierre = int.Parse(value.RegUsuario.ToString()), RegUsuario = value.RegUsuario, RegEstacion = value.RegEstacion };
                        var result = await _repository.TxSIM.UpdateStatus(updateStatus.RetornaTxSIM());

                        if (result.ResultadoCodigo == -1)
                        {
                            return BadRequest(result);
                        }
                    }
                }

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
        public async Task<IActionResult> Update([FromBody] DtoUpdateTxSim value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSIM.Update(value.RetornaTxSIM());

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
        public async Task<IActionResult> UpdateStatus([FromBody] DtoUpdateStatusTxSim value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.TxSIM.UpdateStatus(value.RetornaTxSIM());

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
        public async Task<IActionResult> Delete([FromBody] DtoDeleteTxSim value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxSIM.Delete(value.RetornaTxSIM());

            return NoContent();
        }

        /// <summary>
        /// Obtener un TxRegistro Equipo
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetGeneraPdfByIdTxSIM")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<FileContentResult> GetGeneraPdfByIdTxSIM(int id)
        {
            var objectGetById = await _repository.TxSIM.GenerarPDF(new DtoFindTxSimPorId { IdSIM = id }.RetornaTxSIM());

            var pdf = File(objectGetById.GetBuffer(), "applicacion/pdf", "diploma.pdf");

            return pdf;
        }
    }
}
