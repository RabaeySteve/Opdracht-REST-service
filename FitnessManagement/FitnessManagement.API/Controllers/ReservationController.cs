using FitnessBL.Models;
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
    public class ReservationController : ControllerBase {
        private ReservationService ReservationRepo;

        public ReservationController(ReservationService reservationRepo) {
            this.ReservationRepo = reservationRepo;

        }

        [HttpGet("{id}")]
        public ActionResult<List<Reservation>> Get(int id) {
            try {
                return ReservationRepo.GetReservation(id);
            } catch (ReservationException ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<List<ReservationDTO>> Post(ReservationDTO reservationDTO) {
            ReservationRepo.AddReservation(ReservationMapper.MapReservation(reservationDTO), reservationDTO.DubleReservation);
            return CreatedAtAction(nameof(Get), new { id =  reservationDTO.MemberId}, reservationDTO);
        }

       
    }
}
