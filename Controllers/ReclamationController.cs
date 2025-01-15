using ClaimTrack.NetBackend.Context;
using ClaimTrack.NetBackend.Dto;
using ClaimTrack.NetBackend.Models;
using ClaimTrack.NetBackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaimTrack.NetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamationController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IReclamationRepository _reclamationRepository;

        public ReclamationController(IReclamationRepository reclamationRepository, AppDbContext context)
        {
            _reclamationRepository = reclamationRepository;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReclamation(ReclamationDTO reclamationDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(reclamationDTO.IdUser);
                var article = await _context.ArticlesVendus.FindAsync(reclamationDTO.IdArticle);

                if (user == null)
                {
                    return NotFound($"Utilisateur avec l'ID {reclamationDTO.IdUser} non trouvé.");
                }

                if (article == null)
                {
                    return NotFound($"Article avec l'ID {reclamationDTO.IdArticle} non trouvé.");
                }

                var reclamation = new Reclamation
                {
                    Sujet = reclamationDTO.Sujet,
                    Description = reclamationDTO.Description,
                    DateReclamation = reclamationDTO.DateReclamation,
                    Statut = reclamationDTO.Statut,
                    IdUser = reclamationDTO.IdUser,
                    User = user,
                    IdArticle = reclamationDTO.IdArticle,
                    Article = article,
                    IdIntervention = null,
                    Intervention = null
                };

                _context.Reclamations.Add(reclamation);
                await _context.SaveChangesAsync();

                var reclamationDtoResponse = new ReclamationDTO
                {
                    Sujet = reclamation.Sujet,
                    Description = reclamation.Description,
                    DateReclamation = reclamation.DateReclamation,
                    Statut = reclamation.Statut,
                    IdUser = reclamation.IdUser,
                    IdArticle = reclamation.IdArticle,
                };

                return CreatedAtAction(
                    nameof(GetReclamationById),
                    new { id = reclamation.Id },
                    reclamationDtoResponse
                );
            }

            return BadRequest(ModelState);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReclamationDTO>> GetReclamationById(int id)
        {
            var reclamation = await _reclamationRepository.GetByIdAsync(id);

            if (reclamation == null)
            {
                return NotFound($"Reclamation avec l'ID {id} non trouvée.");
            }

            // Mapper les données vers un DTO
            var reclamationDto = new ReclamationDTO
            {
                Sujet = reclamation.Sujet,
                Description = reclamation.Description,
                DateReclamation = reclamation.DateReclamation,
                Statut = reclamation.Statut,
                IdUser = reclamation.IdUser,
                IdArticle = reclamation.IdArticle,
                IdIntervention = reclamation.IdIntervention
            };

            return Ok(reclamationDto);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReclamationDTO>>> GetAllReclamations()
        {
            var reclamations = await _reclamationRepository.GetAllAsync();

            var reclamationsDto = reclamations.Select(r => new ReclamationDTO
            {
                Sujet = r.Sujet,
                Description = r.Description,
                DateReclamation = r.DateReclamation,
                Statut = r.Statut,
                IdUser = r.IdUser,
                IdArticle = r.IdArticle,
                IdIntervention = r.IdIntervention
            }).ToList();

            return Ok(reclamationsDto);
        }
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateReclamation(int id, ReclamationDTO reclamationDTO)
        //{
        //    var existingReclamation = await _reclamationRepository.GetByIdAsync(id);
        //    if (existingReclamation == null)
        //    {
        //        return NotFound($"Reclamation with ID {id} not found.");
        //    }
        //    var user = await _context.Users.FindAsync(reclamationDTO.IdUser);
        //    if (user == null)
        //    {
        //        return NotFound($"User with ID {reclamationDTO.IdUser} not found.");
        //    }
        //    var article = await _context.ArticlesVendus.FindAsync(reclamationDTO.IdArticle);
        //    if (article == null)
        //    {
        //        return NotFound($"Article with ID {reclamationDTO.IdArticle} not found.");
        //    }
        //    existingReclamation.Sujet = reclamationDTO.Sujet;
        //    existingReclamation.Description = reclamationDTO.Description;
        //    existingReclamation.DateReclamation = reclamationDTO.DateReclamation;
        //    existingReclamation.Statut = reclamationDTO.Statut;
        //    existingReclamation.IdUser = reclamationDTO.IdUser;
        //    existingReclamation.IdArticle = reclamationDTO.IdArticle;
        //    existingReclamation.IdIntervention = reclamationDTO.IdIntervention;

        //    try
        //    {
        //        await _reclamationRepository.UpdateAsync(existingReclamation);
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
        //    }
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReclamation(int id, ReclamationDTO reclamationDTO)
        {
            var existingReclamation = await _reclamationRepository.GetByIdAsync(id);
            if (existingReclamation == null)
            {
                return NotFound($"Reclamation with ID {id} not found.");
            }

            // Validate foreign keys
            var userExists = await _context.Users.AnyAsync(u => u.Id == reclamationDTO.IdUser);
            if (!userExists)
            {
                return BadRequest($"User with ID {reclamationDTO.IdUser} does not exist.");
            }

            var articleExists = await _context.ArticlesVendus.AnyAsync(a => a.Id == reclamationDTO.IdArticle);
            if (!articleExists)
            {
                return BadRequest($"Article with ID {reclamationDTO.IdArticle} does not exist.");
            }

            if (reclamationDTO.IdIntervention != null && reclamationDTO.IdIntervention != 0) // Validate intervention if ID is valid
            {
                var intervention = await _context.Interventions.FindAsync(reclamationDTO.IdIntervention);
                if (intervention == null)
                {
                    return BadRequest($"Intervention with ID {reclamationDTO.IdIntervention} does not exist.");
                }

                // Update the intervention reference
                existingReclamation.IdIntervention = reclamationDTO.IdIntervention;
                existingReclamation.Intervention = intervention;  // Associate the intervention object
            }
            else
            {
                // If no intervention is provided or intervention ID is 0, disassociate the intervention
                existingReclamation.IdIntervention = null;
                existingReclamation.Intervention = null;  // Set the navigation property to null
            }

            // Update remaining fields
            existingReclamation.Sujet = reclamationDTO.Sujet;
            existingReclamation.Description = reclamationDTO.Description;
            existingReclamation.DateReclamation = reclamationDTO.DateReclamation;
            existingReclamation.Statut = reclamationDTO.Statut;
            existingReclamation.IdUser = reclamationDTO.IdUser;
            existingReclamation.IdArticle = reclamationDTO.IdArticle;

            try
            {
                await _reclamationRepository.UpdateAsync(existingReclamation);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReclamation(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Reclamation ID.");
            }

            var existingReclamation = await _reclamationRepository.GetByIdAsync(id);
            if (existingReclamation == null)
            {
                return NotFound($"Reclamation with ID {id} not found.");
            }

            try
            {
                await _reclamationRepository.DeleteAsync(id);
                return NoContent(); // HTTP 204, suppression réussie sans contenu retourné
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
