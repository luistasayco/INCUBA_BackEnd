﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Net.Data;
using Net.Business.DTO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Net.Business.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "ApiDashboardFormulario")]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class DashboardFormularioController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public DashboardFormularioController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="planta"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] DtoFindDashboardFormulario value, string planta)
        {

            if (string.IsNullOrEmpty(planta))
            {
                value.ListPlanta = new List<DtoEmpresaPlanta>();
            } else {
                value.ListPlanta = JsonConvert.DeserializeObject<List<DtoEmpresaPlanta>>(planta);
            }

            var objectGetAll = await _repository.DashboardFormulario.GetAll(value.DashboardFormularioPorFiltro());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);

        }
    }
}
