using ClaimTrack.NetBackend.Dto;
using ClaimTrack.NetBackend.Models;
using ClaimTrack.NetBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClaimTrack.NetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly IInterventionRepository _interventionRepository;

        public InterventionsController(IInterventionRepository interventionRepository)
        {
            _interventionRepository = interventionRepository;
        }

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventions()
        {
            var interventions = await _interventionRepository.GetAllInterventionsAsync();
            return Ok(interventions);
        }

        // GET: api/Interventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(int id)
        {
            var intervention = await _interventionRepository.GetInterventionByIdAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            return Ok(intervention);
        }

        // POST: api/Interventions
        [HttpPost]
        public async Task<ActionResult<Intervention>> CreateIntervention([FromBody] InterventionDTO interventionDto)
        {
            if (interventionDto == null)
            {
                return BadRequest("Les données de l'intervention sont invalides.");
            }

            // Créer une intervention à partir du DTO
            var intervention = new Intervention
            {
                ReclamationId = interventionDto.ReclamationId,

                Technicien = interventionDto.Technicien,
                Duree = interventionDto.Duree,
                PieceRechangeId = interventionDto.PieceRechangeId,
                DateIntervention = interventionDto.DateIntervention
            };

            // Créer l'intervention via le repository
            await _interventionRepository.CreateInterventionAsync(intervention);

            return CreatedAtAction(nameof(GetIntervention), new { id = intervention.Id }, intervention);
        }

        // PUT: api/Interventions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIntervention(int id, [FromBody] InterventionDTO interventionDto)
        {
            if (id != interventionDto.Id)
            {
                return BadRequest();
            }

            var intervention = await _interventionRepository.GetInterventionByIdAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            // Mettre à jour l'intervention
            intervention.Technicien = interventionDto.Technicien;
            intervention.Duree = interventionDto.Duree;
            intervention.PieceRechangeId = interventionDto.PieceRechangeId;
            intervention.DateIntervention = interventionDto.DateIntervention;

            await _interventionRepository.UpdateInterventionAsync(intervention);

            return NoContent();
        }

        // DELETE: api/Interventions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntervention(int id)
        {
            var intervention = await _interventionRepository.GetInterventionByIdAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            await _interventionRepository.DeleteInterventionAsync(id);
            return NoContent();
        }
    }
}
