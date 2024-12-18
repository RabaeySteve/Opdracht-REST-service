using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Mapper;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.BL.Services;
using FitnessManagement.EF.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase {

        private MemberService MemberRepo;
        //private ReservationService ReservationRepo;

        public MemberController(MemberService memberRepo/*, ReservationService reservationRepo*/) {
            this.MemberRepo = memberRepo;
            //this.ReservationRepo = reservationRepo;
        }

        [HttpGet("{id}")]
        public ActionResult<MemberDTO> Get(int id) {
            try {
              return  MemberMapper.ToMemberDTO(MemberRepo.GetMember(id));
            } catch (MemberException ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpGet(("{id}/Programs"))]
        public ActionResult<MemberProgramsDTO> GetMemberPrograms(int id) {
            try {
                Member member = MemberRepo.GetMember(id);
                MemberProgramsDTO programsDTO = new MemberProgramsDTO {
                    MemberId= member.MemberId,
                    ProgramsList = member.Programs
                };
                return programsDTO;
                
            } catch (MemberException ex) {

                throw new MemberException("");
            }
        }
        //[HttpGet("{id}/Reservations")]
        //public ActionResult<MemberReservationsDTO> GetMemberReservations(int id) {
        //    try {
        //        return MemberMapper.MapReservationsToMember(ReservationRepo.GetReservationsMember(id));
        //    } catch (Exception) {

        //        return NotFound();
        //    }
        //}

        [HttpPost("Create-Member")]
        public ActionResult<Member> Post(MemberDTO member) {
            MemberRepo.AddMember(MemberMapper.ToMember(member));
            return CreatedAtAction(nameof(Get), new {id = member.MemberId}, member);
        }

        [HttpPost("Add-Program")]
        public ActionResult<Member> Post(MemberAddProgramDTO program) {
            MemberRepo.AddProgram(program.MemberId, program.ProgramCode);
            return CreatedAtAction(nameof(Get), new { id = program.MemberId }, program);
        }
        

        [HttpPut]
        public IActionResult Put(int id, [FromBody] MemberDTO member) {
            if (member == null || member.MemberId != id) {
                return BadRequest();
            }
            if (!MemberRepo.IsMember(id)) {
                MemberRepo.AddMember(MemberMapper.ToMember(member));
                return CreatedAtAction(nameof(Get), new { id = member.MemberId }, member);
            }
            MemberRepo.UpdateMember(MemberMapper.ToMember(member));
            return new NoContentResult();
        }
        
    }
}
