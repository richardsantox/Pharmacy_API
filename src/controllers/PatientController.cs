using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.src.dtos;
using Pharmacy.src.models;
using Pharmacy.src.repositories;
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<ActionResult> NewPatientAsync([FromBody] PatientDTO patient)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewPatientAsync(patient);
            return Created($"api/Patient/name/{patient.Name}", patient);
        }


        /// <summary>
        /// Pegar todos os pacientes
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de pacientes</response>
        /// <response code="204">Lista vazia</response>
        [HttpGet("GetAllPatiens")]
        public async Task<ActionResult> GetAllPatiensAsync()
        {
            var list = await _repository.GetAllPatiensAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Pegar todos os medicamentos tomados
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de medicamentos tomados</response>
        /// <response code="204">Lista vazia</response>
        [HttpGet("medicineTakens")]
        public async Task<ActionResult> GetAllMedicineTakensAsync(string name)
        {
            var list = await _repository.GetAllMedicineTakensAsync(name);

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        #endregion

    }
}
