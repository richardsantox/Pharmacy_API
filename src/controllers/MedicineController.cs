using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.src.dtos;
using Pharmacy.src.models;
using Pharmacy.src.repositories;
using System;
using System.Threading.Tasks;

namespace Pharmacy.src.controllers
{
    [ApiController]
    [Route("api/Medicine")]
    [Produces("application/json")]
    public class MedicineController : ControllerBase
    {
        #region Attributes

        private readonly IMedicine _repository;


        #endregion


        #region Builders

        public MedicineController(IMedicine repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Resumo: Criar novo Medicamento
        /// </summary>
        /// <param name="medicine">MedicineDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Medicine
        ///     {
        ///        "name": "Dorflex"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna medicamento criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">Paciente ja cadastrado</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MedicineDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("Register")]
        public async Task<ActionResult> NewMedicineAsync([FromBody] MedicineDTO medicine)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                await _repository.NewMedicineAsync(medicine);

                return Created($"api/Medicine/name/{medicine.Name}", medicine);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

            
        }


        /// <summary>
        /// Pegar todos os medicamentos
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de medicamentos</response>
        /// <response code="204">Lista vazia</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetAllMedicines")]
        public async Task<ActionResult> GetAllMedicinesAsync()
        {
            var list = await _repository.GetAllMedicinesAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }


        /// <summary>
        /// Pegar todos os pacientes que tomaram o medicamento
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de pacientes que tomaram o medicamento</response>
        /// <response code="204">Lista vazia</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetAllPatientsWhoTook")]
        public  async Task<ActionResult> GetAllPatientsWhoTookAsync([FromQuery] string name)
        {
            return Ok(await _repository.GetAllPatientsWhoTookAsync(name));
        }

        /// <summary>
        /// Pegar quantidade de pacientes que cada medicamento teve
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Quantidade de pacientes consumistas por medicamentos</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("NumberPatientWhoTook")]
        public async Task<ActionResult<int>> NumberPatientsWhoHaveAlreadyTakenTheDrugAsync()
        {
            return Ok(await _repository.NumberPatientsWhoHaveAlreadyTakenTheDrugAsync());
        }

        #endregion
    }
}
