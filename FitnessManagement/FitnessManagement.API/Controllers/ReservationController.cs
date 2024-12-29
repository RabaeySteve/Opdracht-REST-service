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
                return Ok(reservationDTO);
            } catch (Exception ex) {

                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}/{date}")]
        public ActionResult<List<ReservationGetDTO>> GetMemberDate(int id, string date) {
            try {
                if (!DateOnly.TryParse(date, out var parsedDate)) {
                    return BadRequest("Invalid date format. Please use 'yyyy-MM-dd'.");
                }
                List<Reservation> reservations = ReservationRepo.GetReservationMemberDate(id, parsedDate);
                var result = new List<ReservationGetDTO>();
                foreach (Reservation reservation in reservations) {
                    
                    var mappedReservation = ReservationMapper.MapToGetDTO(reservation);
                    
                    result.Add(mappedReservation);
                }
                if (result == null) {
                    return NotFound();
                }
                return Ok(result);
            } catch (Exception ex) {

                return NotFound(ex.Message);
            }
        }
       
        [HttpGet("{date}/AvailableTimeSlots")]
        public ActionResult<Dictionary<int, List<EquipmentString>>> GetAvailableTimeSlots(string date) {
            try {
                // Controleer of de datum geldig is
                if (!DateOnly.TryParse(date, out var parsedDate)) {
                    return BadRequest("Invalid date format. Please use 'yyyy-MM-dd'.");
                }

                // Haal de beschikbare tijdsloten op
                Dictionary<int, List<Equipment>> availableTimeSlots = ReservationRepo.AvailableTimeSlotDate(parsedDate);

                // Map elke lijst van Equipment naar EquipmentString
                Dictionary<int, List<EquipmentString>> mappedTimeSlots = availableTimeSlots.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Select(equipment => EquipmentMapper.MapEquipmentTypeToStirng(equipment)).ToList()
                );

                return Ok(mappedTimeSlots);
            } catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }



        [HttpPost]
        public ActionResult<ReservationPostDTO> Post(ReservationPostDTO reservationDTO) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try {
                ReservationRepo.AddReservation(ReservationMapper.MapPostReservation(reservationDTO));
                return CreatedAtAction(nameof(Get), new { id = reservationDTO.MemberId }, reservationDTO);
            } catch (Exception ex) {
                return Conflict(new { message = ex.Message });
            }
           
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
                return BadRequest("Data cannot be null.");
            }
            if (!ReservationRepo.IsReservation(id)) {
                return NotFound($"Reservation with ID {id} does not exist.");
            }
            try {
                ReservationRepo.UpdateReservation(ReservationMapper.MapDTOToReservation(reservationPutDTO));
                return new NoContentResult();
            } catch (Exception ex) {
                return Conflict(new { message = ex.Message });
            }
           
        }


    }
}
