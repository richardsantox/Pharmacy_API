using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.src.dtos;
using Pharmacy.src.models;
using Pharmacy.src.repositories;
using System.Threading.Tasks;

namespace Pharmacy.src.controllers
{
    [ApiController]
    [Route("api/MedicationControl")]
    [Produces("application/json")]
    public class MedicationControlController : ControllerBase
    {

        #region Attributes

        private readonly IMedicationControl _repository;

        #endregion


        #region Builders

        public MedicationControlController(IMedicationControl medicationControl)
        {
            _repository = medicationControl;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Resumo: Criar novo Controle de Medicamento
        /// </summary>
        /// <param name="medicationControl">MedicationControlDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/MedicationControl
        ///     {
        ///         "patient": {
        ///             "name": "Richard Santos"
        ///         },
        ///          "medicineTaken": {
        ///            "name": "Dorflex"
        ///          }
        ///      }
        ///
        /// </remarks>
        /// <response code="201">Retorna controle criado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MedicationControlDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Register")]
        public async Task<ActionResult> NewControlAsync([FromBody] MedicationControlDTO medicationControl)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repository.NewControlAsync(medicationControl);
            return Created($"api/MedicationControl", medicationControl);
        }

        /// <summary>
        /// Pegar todos os controles de medicamentos
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de controle</response>
        /// <response code="204">Lista vasia</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetAllControlData")]
        public async Task<ActionResult> GetTheControlDataAsync()
        {
            return Ok(await _repository.GetTheControlDataAsync());
        }

        #endregion
    }
}
