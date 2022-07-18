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
    [Route("api/Patient")]
    [Produces("application/json")]
    public class PatientController : ControllerBase
    {

        #region Attributes

        private readonly IPatient _repository;

        #endregion


        #region Builders

        public PatientController(IPatient repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Resumo: Criar novo Paciente
        /// </summary>
        /// <param name="patient">PatientDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Patient
        ///     {
        ///        "name": "Richard Santos",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna paciente criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">Paciente ja cadastrado</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("Register")]
        public async Task<ActionResult> NewPatientAsync([FromBody] PatientDTO patient)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                await _repository.NewPatientAsync(patient);

                return Created($"api/Patient/name/{patient.Name}", patient);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }


        /// <summary>
        /// Pegar todos os pacientes
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de pacientes</response>
        /// <response code="204">Lista vazia</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetAllPatients")]
        public async Task<ActionResult> GetAllPatiensAsync()
        {
            var list = await _repository.GetAllPatiensAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Pegar todos os medicamentos tomados por um paciente
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de medicamentos tomados por um paciente</response>
        /// <response code="204">Lista vazia</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetAllMedicineTakens")]
        public async Task<ActionResult> GetAllMedicineTakensAsync([FromQuery] string name)
        {
            return Ok(await _repository.GetAllMedicineTakensAsync(name));
        }

        /// <summary>
        /// Pegar quantidade de medicação que cada paciente tomou
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Quantidade de medicamentos tomados por paciente</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("NumberMedicineTakens")]
        public async Task<ActionResult> AmounMedicationPatientsWhoHaveAlreadyTakenAsync()
        {
            return Ok( await _repository.AmounMedicationPatientsWhoHaveAlreadyTakenAsync());
        }

        #endregion
    }
}
