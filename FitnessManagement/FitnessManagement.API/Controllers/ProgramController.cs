using FitnessManagement.API.Mapper;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase {
        private ProgramService ProgramService;
        
        public ProgramController(ProgramService programService) {
            this.ProgramService = programService;
        }


        [HttpGet("{programCode}")]
        public ActionResult<FitnessManagement.BL.Models.Program> Get(string programCode) {
            try {
                return ProgramService.GetProgramByProgramCode(programCode);
                
            } catch (ProgramException ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<FitnessManagement.BL.Models.Program> Post(FitnessManagement.BL.Models.Program program) {
            ProgramService.AddProgram(program);
            return CreatedAtAction(nameof(Get), new { programCode = program.ProgramCode }, program);
        }
        [HttpPut]
        public IActionResult Put(string programCode, [FromBody] FitnessManagement.BL.Models.Program program) {
            if (program == null || program.ProgramCode != programCode) {
                return BadRequest();
            }

            ProgramService.UpdateProgram(program);
            return new NoContentResult();
        }
    }
}
