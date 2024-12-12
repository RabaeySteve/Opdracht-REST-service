using FitnessBL.Models;

namespace FitnessManagement.API.DTO_s {
    public class ReservationDTO {

        public int MemberId { get; set; }
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public int StartTime { get; set; }
      
        public int EquipmentId { get; set; }

        public bool DubleReservation { get; set; }
    }
}
