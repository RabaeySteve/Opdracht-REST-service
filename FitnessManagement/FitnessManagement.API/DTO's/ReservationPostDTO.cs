using FitnessBL.Models;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.DTO_s {

    public class ReservationPostDTO {
        public int MemberId { get; set; }
        public DateOnly Date { get; set; }
        public List<TimeSlotEquipmentDTO> Reservations { get; set; }
    }
    public class TimeSlotEquipmentDTO {
        public int TimeSlotId { get; set; }
        public int EquipmentId { get; set; }
    }
    public class TimeSlotEquipmentGetDTO {
        public TimeSlot TimeSlot { get; set; }
        public Equipment Equipment { get; set; }

    }
    public class ReservationGetDTO {
        public int ReservationId { get; set; }
        public int GroupsId { get; set; }
        public int MemberId { get; set; }
        public DateOnly Date { get; set; }
        public List<TimeSlotEquipmentGetDTO> Reservations { get; set; }
    }

    public class ReservationPutDTO {
        public int ReservationId { get; set; }
        public int GroupsId { get; set; }
        public int MemberId { get; set; }
        public DateOnly Date { get; set; }
        public List<TimeSlotEquipmentDTO> Reservations { get; set; }
    }




}
