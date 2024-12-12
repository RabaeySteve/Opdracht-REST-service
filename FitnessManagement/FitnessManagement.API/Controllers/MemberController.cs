using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Mapper;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase {

        private MemberService MemberRepo;
        
        public MemberController(MemberService memberRepo) {
            this.MemberRepo = memberRepo;
            
        }

        [HttpGet("{id}")]
        public ActionResult<Member> Get(int id) {
            try {
              return  MemberRepo.GetMember(id);
            } catch (MemberException ex) {

                return NotFound(ex.Message);
            }
        }
        
        [HttpPost]
        public ActionResult<Member> Post(AddMemberDTO member) {
            MemberRepo.AddMember(MemberMapper.ToMember(member));
            return CreatedAtAction(nameof(Get), new {id = member.MemberId}, member);
        }
        [HttpPut]
        public IActionResult Put(int id, [FromBody] AddMemberDTO member) {
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
