﻿using System.Collections.Generic;
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
    [ApiExplorerSettings(GroupName = "ApiMaestroExamenPollito")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TxRegistroDocumentoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public TxRegistroDocumentoController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Obtener lista de calidad
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de Calidad registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindTxRegistroDocumento value)
        {

            var objectGetAll = await _repository.TxRegistroDocumento.GetAll(value.RetornaTxRegistroDocumento());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }
        /// <summary>
        /// Obtener una calidad individual
        /// </summary>
        /// <param name="id">Id de Calidad</param>
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        //[HttpGet("{id}", Name = "GetbyIdTxRegistroDocumento")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> GetbyIdTxRegistroDocumento(int id)
        //{
        //    var objectGetById = await _repository.TxRegistroDocumento.GetById(new DtoFindTxRegistroDocumento { IdDocumento = id }.RetornaTxRegistroDocumento());

        //    if (objectGetById == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(objectGetById);
        //}

        /// <summary>
        /// Crear una nueva calidad
        /// </summary>
        /// <param name="value"></param>
        /// <param name="archivo"></param>
        /// <returns>Id del registro creado</returns>
        /// <response code="201">Devuelve el elemento recién creado</response>
        /// <response code="400">Si el objeto enviado es nulo o invalido</response>  
        /// <response code="500">Algo salio mal guardando el registro</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromForm] string value, [FromForm] IList<IFormFile> archivo)
        {
            DtoInsertTxRegistroDocumento myObj = JsonConvert.DeserializeObject<DtoInsertTxRegistroDocumento>(value);

            if (myObj == null)
            {
                return BadRequest("Master object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            int ObjectNew = await _repository.TxRegistroDocumento.Create(myObj.RetornaTxRegistroDocumento(), archivo);

            if (ObjectNew == 0)
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro {myObj.NombreArchivo}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        /// <summary>
        /// Eliminar logicamente una calidad
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
        public async Task<IActionResult> Delete([FromBody] DtoDeleteTxRegistroDocumento value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.TxRegistroDocumento.Delete(value.RetornaTxRegistroDocumento());

            return NoContent();
        }

        /// <summary>
        /// Obtener un TxRegistro Equipo
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetDownloadFile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<FileContentResult> GetDownloadFile(string id)
        {
            var objectGetById = await _repository.TxRegistroDocumento.GetDownloadFileGoogleDrive(new DtoFindTxRegistroDocumentoId { IdGoogleDrive  = id }.RetornaTxRegistroDocumento());

            var pdf = File(objectGetById.FileMemoryStream.GetBuffer(), objectGetById.TypeFile, objectGetById.NameFile);

            return pdf;
        }

    }
}