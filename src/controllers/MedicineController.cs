using Microsoft.AspNetCore.Mvc;
using Pharmacy.src.dtos;
using Pharmacy.src.models;
using Pharmacy.src.repositories;
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
        ///        "name": "Dorflex",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna medicamento criado</response>
        /// <response code="400">Erro na requisição</response>
        [HttpPost("register")]
        public async Task<ActionResult> NewMedicineAsync([FromBody] MedicineDTO medicine)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewMedicineAsync(medicine);
            return Created($"api/Medicine/name/{medicine.Name}", medicine);
        }


        /// <summary>
        /// Pegar todos os medicamentos
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de medicamentos</response>
        /// <response code="204">Lista vazia</response>
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
        [HttpGet("patientsWhoTook")]
        public  async Task<ActionResult> GetAllPatientsWhoTookAsync(string name)
        {
            var list = await _repository.GetAllPatientsWhoTookAsync(name);

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        #endregion
    }
}
