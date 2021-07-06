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
    [ApiExplorerSettings(GroupName = "ApiVacunacionSubCutanea")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TxVacunacionSubCutaneaController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public TxVacunacionSubCutaneaController(IRepositoryWrapper repository)
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
        public async Task<IActionResult> GetAll([FromQuery] DtopFindTxVacunacionSubCutanea value)
        {

            var objectGetAll = await _repository.TxVacunacionSubCutanea.GetAll(value.RetornaTxVacunacionSubCutanea());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
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
        public async Task<IActionResult> GetByDetalleNew()
        {

            var objectGetAll = await _repository.TxVacunacionSubCutanea.GetByIdNew(new DtoFindTxVacunacionSubCutaneaPorId { IdVacunacionSubCutanea = 0 }.RetornaTxVacunacionSubCutanea());

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
        [HttpGet("{id}", Name = "GetByIdTxVacunacionSubCutanea")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByIdTxVacunacionSubCutanea(int id)
        {
            var objectGetById = await _repository.TxVacunacionSubCutanea.GetById(new DtoFindTxVacunacionSubCutaneaPorId { IdVacunacionSubCutanea = id }.RetornaTxVacunacionSubCutanea());

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DtoInsertTxVacunacionSubCutanea value)
        {
            if (value == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var ObjectNew = await _repository.TxVacunacionSubCutanea.Create(value.RetornaTxVacunacionSubCutanea());

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
                        var updateStatus = new DtoUpdateStatusTxVacunacionSubCutanea { IdVacunacionSubCutanea = ObjectNew.IdRegistro, IdUsuarioCierre = int.Parse(value.RegUsuario.ToString()), RegUsuario = value.RegUsuario, RegEstacion = value.RegEstacion };
                        var result = await _repository.TxVacunacionSubCutanea.UpdateStatus(updateStatus.RetornaTxVacunacionSubCutanea());

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
        public async Task<IActionResult> Update([FromBody] DtoUpdateTxVacunacionSubCutanea value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxVacunacionSubCutanea.Update(value.RetornaTxVacunacionSubCutanea());

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
        public async Task<IActionResult> UpdateStatus([FromBody] DtoUpdateStatusTxVacunacionSubCutanea value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.TxVacunacionSubCutanea.UpdateStatus(value.RetornaTxVacunacionSubCutanea());

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
        public async Task<IActionResult> Delete([FromBody] DtoDeleteTxVacunacionSubCutanea value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxVacunacionSubCutanea.Delete(value.RetornaTxVacunacionSubCutanea());

            return NoContent();
        }

        /// <summary>
        /// Obtener un TxRegistro Equipo
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetGeneraPdfByIdTxVacunacionSubCutanea")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<FileContentResult> GetGeneraPdfByIdTxVacunacionSpray(int id)
        {
            var objectGetById = await _repository.TxVacunacionSubCutanea.GenerarPDF(new DtoFindTxVacunacionSubCutaneaPorId { IdVacunacionSubCutanea = id }.RetornaTxVacunacionSubCutanea());

            var pdf = File(objectGetById.GetBuffer(), "applicacion/pdf", "diploma.pdf");

            return pdf;
        }
    }
}
