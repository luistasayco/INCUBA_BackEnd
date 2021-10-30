using Net.Business.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Net.Data;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Net.Business.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "ApiDashboardAuditoria")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DashboardAuditoriaController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public DashboardAuditoriaController(IRepositoryWrapper repository) 
        {
            this._repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery]DateTime FechaInicio, DateTime FechaFin, int idDashboard, int tipo, string planta, string responsableInvetsa, string responsablePlanta, string lineaGenetica, string vacunador,int idUsuario) 
        {

            DtoFindDashboardAuditoria value = new DtoFindDashboardAuditoria
            {
                FechaInicio = FechaInicio,
                FechaFin = FechaFin,
                IdDashboard = idDashboard,
                Tipo = tipo,
                ListResponsableInvetsa = JsonConvert.DeserializeObject<List<DtoResponsableInvetsa>>(responsableInvetsa),
                ListResponsablePlanta = JsonConvert.DeserializeObject<List<DtoResponsablePlanta>>(responsablePlanta),
                ListPlanta = JsonConvert.DeserializeObject<List<DtoEmpresaPlanta>>(planta),
                ListLineaGenetica = JsonConvert.DeserializeObject<List<DtoLineaGenetica>>(lineaGenetica),
                ListVacunador = JsonConvert.DeserializeObject<List<DtoVacunador>>(vacunador),
                IdUsuario = idUsuario,
            };

            var objectGetAll = await _repository.DashboardAuditoria.GetAll(value.General());
            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

    }
}
