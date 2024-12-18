using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Mapper;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Models;
using FitnessManagement.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RunningSessionController : ControllerBase {
        private RunningSessionService RunningRepo;

        public RunningSessionController(RunningSessionService runningRepo) {
            this.RunningRepo = runningRepo;
        }

        [HttpGet("{id}")]
        public ActionResult<RunningSessionDTO> Get(int id) {
            try {
                RunningSession runningSession = RunningRepo.GetById(id);
                RunningSessionDTO runningSessionDTO = runningSessionDTO = RunningSessionMapper.MapToRunningDTO(runningSession);

                return runningSessionDTO;
            } catch (CyclingSessionException ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<RunningSessionDTO> Post(RunningSessionDTO runningSessionDTO) {
            RunningRepo.AddSession(RunningSessionMapper.MapRunningSession(runningSessionDTO));
            return CreatedAtAction(nameof(Get), new { id = runningSessionDTO.RunningSessionId }, runningSessionDTO);
        }
       
    }
}
