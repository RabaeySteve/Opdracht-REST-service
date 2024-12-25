using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Mapper;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Models;
using FitnessManagement.BL.Services;
using FitnessManagement.EF.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase {
        private ReservationService ReservationRepo;

        public ReservationController(ReservationService reservationRepo) {
            this.ReservationRepo = reservationRepo;

        }

        [HttpGet("{id}")]
        public ActionResult<ReservationGetDTO> Get(int id) {
            try {
                Reservation reservation =  ReservationRepo.GetReservation(id);
                ReservationGetDTO reservationDTO = ReservationMapper.MapToGetDTO(reservation);
                return reservationDTO;
            } catch (ReservationException ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<ReservationPostDTO> Post(ReservationPostDTO reservationDTO) {

            ReservationRepo.AddReservation(ReservationMapper.MapPostReservation(reservationDTO));
            return CreatedAtAction(nameof(Get), new { id = reservationDTO.MemberId }, reservationDTO);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (!ReservationRepo.IsReservation(id)) {
                return NotFound();
            }
            ReservationRepo.DeleteReservation(id);
            return NoContent();
        }
        [HttpPut]
        public IActionResult Put(int id, [FromBody] ReservationPutDTO reservationPutDTO) {
            if (reservationPutDTO == null ) {
                return BadRequest();
            }
            ReservationRepo.UpdateReservation(ReservationMapper.MapDTOToReservation(reservationPutDTO));
            return new NoContentResult();
        }


    }
}
