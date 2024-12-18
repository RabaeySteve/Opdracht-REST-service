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
    public class CyclingSessionController : ControllerBase {
        private CyclingSessionService CyclingRepo;

        public CyclingSessionController(CyclingSessionService cyclingRepo) {
            this.CyclingRepo = cyclingRepo;
        }

        [HttpGet("{id}")]
        public ActionResult<CyclingSessionDTO> Get(int id) {
            try {
                CyclingSession cyclingSession  = CyclingRepo.GetById(id);
                CyclingSessionDTO cyclingSessionDTO = new CyclingSessionDTO();
                cyclingSessionDTO = CyclingSessionMapper.MapCyclingSessionToDTO(cyclingSession);
                return cyclingSessionDTO;
            } catch (CyclingSessionException ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<CyclingSessionDTO> Post(CyclingSessionDTO cyclingSessionDTO) {
            CyclingRepo.AddSession(CyclingSessionMapper.MapCyclingSession(cyclingSessionDTO));
            return CreatedAtAction(nameof(Get), new {id = cyclingSessionDTO.CyclingSessionId}, cyclingSessionDTO);
        }
      
    }
}
