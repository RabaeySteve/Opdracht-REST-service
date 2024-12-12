using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.Mapper {
    public class ReservationMapper {

        public static Reservation MapReservation(ReservationDTO reservationDTO) {
            return new Reservation {
                Member = IdToMember(reservationDTO.MemberId),
                ReservationId = reservationDTO.ReservationId,
                Date = reservationDTO.Date,
                TimeSlotRes = StartTimeToTimeSlot(reservationDTO.StartTime),
                Equipment = IdToEquipment(reservationDTO.EquipmentId)
            };
        }

        public static TimeSlot StartTimeToTimeSlot(int startTime) {
            return new TimeSlot {
                TimeSlotId = startTime -7,
            };
        }
        public static Member IdToMember(int memberId) {
            return new Member {
                MemberId = memberId
            };
        }
        public static Equipment IdToEquipment(int equipmentId) {
            return new Equipment {
                EquipmentId = equipmentId
            };
        }

    }
}
