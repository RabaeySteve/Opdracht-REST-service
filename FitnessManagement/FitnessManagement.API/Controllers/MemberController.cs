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
        private ReservationService ReservationRepo;

        public MemberController(MemberService memberRepo, ReservationService reservationRepo) {
            this.MemberRepo = memberRepo;
            this.ReservationRepo = reservationRepo;
        }
        [HttpGet]
        [Route("members")]
        public ActionResult<List<MemberDTO>> GetAll() {
            try {
               
                var result = new List<MemberDTO>();
                var members = MemberRepo.GetMembers();

                foreach (var member in members) {
                    var mappedMember = MemberMapper.ToMemberDTO(member);
                    result.Add(mappedMember);
                }

                return Ok(result);
            } catch (MemberException ex) {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<MemberDTO> Get(int id) {
            try {
              return  MemberMapper.ToMemberDTO(MemberRepo.GetMember(id));
            } catch (MemberException ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}/Programs")]
        public ActionResult<MemberProgramsDTO> GetMemberPrograms(int id) {
            try {
                Member member = MemberRepo.GetMember(id);
                MemberProgramsDTO programsDTO = new MemberProgramsDTO {
                    MemberId= member.MemberId,
                    ProgramsList = member.Programs
                };
                return programsDTO;
                
            } catch (MemberException ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}/Reservations")]
        public ActionResult<List<ReservationGetDTO>> GetMemberReservations(int id) {
            try {
                List<Reservation> reservation = ReservationRepo.GetReservationsMember(id);
                List<ReservationGetDTO> reservationGetDTO = reservation.Select(x => ReservationMapper.MapToGetDTO(x)).ToList();
                if (reservation == null || !reservation.Any()) {
                    return NotFound("No reservations found for this member.");
                }

                return reservationGetDTO;
            } catch (Exception ex) {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Members")]
        public ActionResult<Member> Post(MemberDTO member) {
            MemberRepo.AddMember(MemberMapper.ToMember(member));
            return CreatedAtAction(nameof(Get), new {id = member.MemberId}, member);
        }

        [HttpPost("{id}/programs")]
        public ActionResult<Member> Post(MemberAddProgramDTO program) {
            MemberRepo.AddProgram(program.MemberId, program.ProgramCode);
            return CreatedAtAction(nameof(Get), new { id = program.MemberId }, program);
        }
        

        [HttpPut]
        public IActionResult Put(int id, [FromBody] MemberDTO member) {
            if (member == null || member.MemberId != id) {
                return BadRequest();
            }

            MemberRepo.UpdateMember(MemberMapper.ToMember(member));
            return new NoContentResult();
        }
        
    }
}
