using Net.Business.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Net.Data;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Net.Business.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "ApiDashboardSINMI")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DashboardSINMIController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public DashboardSINMIController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] string empresa, string planta, string responsableInvetsa, string responsableCompania, 
            string linea, string edad, int tipoModulo, DateTime fechaInicio, DateTime fechaFin, int idDashboard, int idUsuario)
        {

            DtoFindDashboardSINMI value = new DtoFindDashboardSINMI
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                IdDashboard = idDashboard,
                TipoModulo = tipoModulo,
                ListResponsableInvetsa = JsonConvert.DeserializeObject<List<DtoResponsableInvetsa>>(responsableInvetsa),
                ListResponsablePlanta = JsonConvert.DeserializeObject<List<DtoResponsablePlanta>>(responsableCompania),
                ListEmpresa = JsonConvert.DeserializeObject<List<DtoEmpresa>>(empresa),
                ListPlanta = JsonConvert.DeserializeObject<List<DtoPlanta>>(planta),
                ListLinea = JsonConvert.DeserializeObject<List<DtoLinea>>(linea),
                ListEdad = JsonConvert.DeserializeObject<List<DtoEdad>>(edad),
                IdUsuario = idUsuario,
            };

            var objectGetAll = await _repository.DashboardSINMI.GetAll(value.General());
            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }
    }
}
