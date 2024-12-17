using FitnessBL.Models;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.DTO_s {
   
    public class ReservationDTO {
        public int MemberId { get; set; }
        public DateOnly Date { get; set; } // Datum voor de reservering
        public Dictionary<int, Equipment> Reservations { get; set; } // Tijdslot -> Equipment
    }
    //public class UpddateReservationDTO {

    //    public int MemberId { get; set; }
    //    public int ReservationId { get; set; }

    //    public DateTime Date { get; set; }
    //    public int StartTime { get; set; }

    //    public int EquipmentId { get; set; }

    //    public bool DubleReservation { get; set; }
    //}
}
