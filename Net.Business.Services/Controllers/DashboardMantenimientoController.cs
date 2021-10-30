using System;
using System.Collections.Generic;
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
    [ApiExplorerSettings(GroupName = "ApiDashboardMantenimiento")]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class DashboardMantenimientoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public DashboardMantenimientoController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] DateTime FechaInicio, DateTime FechaFin , int idDashboard, string tecnico, string respuesto, string planta, string modelo, string equipo, int idUsuario)
        {

            DtoFindDashboardMantenimiento value = new DtoFindDashboardMantenimiento
            {
                FechaInicio = FechaInicio,
                FechaFin = FechaFin,
                IdDashboard = idDashboard,
                ListTecnico = JsonConvert.DeserializeObject<List<DtoTecnico>>(tecnico),
                ListRespuesto = JsonConvert.DeserializeObject<List<DtoRespuesto>>(respuesto),
                ListPlanta = JsonConvert.DeserializeObject<List<DtoEmpresaPlanta>>(planta),
                ListModelo = JsonConvert.DeserializeObject<List<DtoModelo>>(modelo),
                ListEquipo = JsonConvert.DeserializeObject<List<DtoEquipo>>(equipo),
                IdUsuario = idUsuario,
            };

            var objectGetAll = await _repository.DashboardMantenimiento.GetAll(value.General());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }
    }
}
