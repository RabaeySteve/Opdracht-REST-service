using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Mapper;
using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase {
        private TrainingService TrainingRepo;

        public TrainingController(TrainingService trainingRepo) {
            this.TrainingRepo = trainingRepo;
        }

        [HttpGet("session")]
        public ActionResult<ITrainingSession> GetSession([FromQuery] int sessionId, [FromQuery] string trainingType) {
            try {
              
                if (sessionId == 0 || string.IsNullOrWhiteSpace(trainingType)) {
                    return BadRequest("Ongeldige inputparameters.");
                }
                ITrainingSession session = TrainingRepo.GetSessionDetails(sessionId, trainingType);

                if (session == null) {
                    return NotFound($"Geen sessie gevonden voor ID {sessionId} en type {trainingType}.");
                }

                if (session is RunningSession runningSession) {
                    RunningSessionDTO runningSessionDTO = RunningSessionMapper.MapToRunningDTO(runningSession);
                    return Ok(runningSessionDTO);
                }
                
                else if (session is CyclingSession cyclingSession) {
                    CyclingSessionDTO cyclingSessionDTO = CyclingSessionMapper.MapCyclingSessionToDTO(cyclingSession);
                    return Ok(cyclingSessionDTO);
                }

               
                return BadRequest($"Ongeldig trainingstype: {trainingType}");
            } catch (Exception ex) {
                
                return StatusCode(500, $"Interne serverfout: {ex.Message}");
            }
        }
        [HttpGet("sessions/month")]
        public ActionResult<List<TrainingImpact>> GetAllMonth([FromQuery] int memberId, [FromQuery] int year, [FromQuery] int month) {
            try {
                if (memberId == 0 || year == 0 || month == 0) {
                    return BadRequest("Ongeldige inputparameters.");
                }

                List<TrainingSession> sessions = TrainingRepo.GetSessionsForCustomerMonth(memberId, year, month);
            
                if (sessions == null || !sessions.Any()) {
                    return NotFound($"Geen sessies gevonden voor klant ID {memberId} in {year}-{month}.");
                }

                return Ok(sessions);
            } catch (Exception ex) {
                return StatusCode(500, $"Interne serverfout: {ex.Message}");
            }
        }
        [HttpGet("Statistics")]
        public ActionResult<TrainingStatistics> GetTrainingStatistics(int memberId) {
            try {
                return TrainingRepo.GetTrainingStatistics(memberId);
            } catch (Exception ex) {

                throw;
            }
        }
        [HttpGet("sessions-per-month")]
        public ActionResult<Dictionary<int, int>> GetSessionsPerMonth([FromQuery] int memberId, [FromQuery] int year) {
            if (memberId <= 0 || year <= 0) {
                return BadRequest("Ongeldige parameters.");
            }

            Dictionary<int, int> result = TrainingRepo.GetMonthlySessionCounts(memberId, year);

            if (result == null || result.Count == 0) {
                return NotFound($"Geen sessies gevonden voor lid ID {memberId} in {year}.");
            }

            return Ok(result);
        }
        [HttpGet("sessions-per-month/Type")]
        public ActionResult<Dictionary<int, int>> GetMonthlySessionCountsType([FromQuery] int memberId, [FromQuery] int year) {
            if (memberId <= 0 || year <= 0 ) {
                return BadRequest("Ongeldige parameters.");
            }

            

            if (result == null || result.Count == 0) {
                return NotFound($"Geen sessies gevonden voor lid ID {memberId} in {year}.");
            }

            return Ok(result);
        }


    }
}
