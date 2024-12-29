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
        public ActionResult<TrainingSessionBase> GetSession([FromQuery] int sessionId, [FromQuery] TrainingSessionType trainingSessionType) {
            try {
              
                if (sessionId == 0) {
                    return BadRequest("Ongeldige inputparameters.");
                }
                var session = TrainingRepo.GetSessionDetails(sessionId, trainingSessionType);

                if (session == null) {
                    return NotFound($"Geen sessie gevonden voor ID {sessionId} en type {trainingSessionType}.");
                }

                if (session is RunningSession runningSession) {
                    RunningSessionDTO runningSessionDTO = RunningSessionMapper.MapToRunningDTO(runningSession);
                    return Ok(runningSessionDTO);
                }
                
                else if (session is CyclingSession cyclingSession) {
                    CyclingSessionDTO cyclingSessionDTO = CyclingSessionMapper.MapCyclingSessionToDTO(cyclingSession);
                    return Ok(cyclingSessionDTO);
                }

               
                return BadRequest($"Ongeldig trainingstype: {trainingSessionType}");
            } catch (Exception ex) {
                
                return StatusCode(500, $"Interne serverfout: {ex.Message}");
            }
        }
        [HttpGet("sessions/month")]
        public ActionResult<List<TrainingMapped>> GetAllMonth([FromQuery] int memberId, [FromQuery] int year, [FromQuery] int month) {
            try {
                if (memberId == 0 || year == 0 || month == 0) {
                    return BadRequest("Ongeldige inputparameters.");
                }

                List<TrainingSessionBase> sessions = TrainingRepo.GetSessionsForCustomerMonth(memberId, year, month);
                List<TrainingMapped> mappesSessions = sessions.Select(x => TrainingMapper.MapTraining(x)).ToList();
                if (sessions == null || !sessions.Any()) {
                    return NotFound($"Geen sessies gevonden voor klant ID {memberId} in {year}-{month}.");
                }

                return Ok(mappesSessions);
            } catch (Exception ex) {
                return StatusCode(500, $"Interne serverfout: {ex.Message}");
            }
        }
        [HttpGet("Statistics")]
        public ActionResult<TrainingStatistics> GetTrainingStatistics(int memberId) {
            try {
                return TrainingRepo.GetTrainingStatistics(memberId);
            } catch (Exception ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpGet("sessions-per-month")]
        public ActionResult<List<MonthlySessionOverview>>GetTrainingStatisticsPerMonth([FromQuery]int memberId, [FromQuery] int year) {
            try {
                return TrainingRepo.GetTrainingStatisticsPerMonth(memberId, year);
            } catch (Exception ex) {

                return NotFound(ex.Message);
            }
          
        }
        [HttpGet("sessions-per-month-summary")]
        public ActionResult<List<MonthlySessionImpact>> GetTrainingStatisticsImpact([FromQuery] int memberId, [FromQuery] int year) {
            try {
                List<MonthlySessionImpact> result = TrainingRepo.GetTrainingStatisticsImpact(memberId, year);

                if (result == null || !result.Any()) {
                    return NotFound($"Geen sessies gevonden voor lid ID {memberId} in {year}.");
                }

                return Ok(result);
            } catch (Exception ex) {
                return StatusCode(500, new { Error = "An error occurred while fetching session summaries.", Details = ex.Message });
            }
        }



    }
}
