using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.Mapper {
    public class ReservationMapper {

        public static List<Reservation> MapReservation(ReservationDTO reservationDTO) {
            var reservations = new List<Reservation>();
            foreach (var kvp in reservationDTO.Reservations) {
                reservations.Add(new Reservation {
                    Member = IdToMember(reservationDTO.MemberId),
                    Date = reservationDTO.Date,
                    TimeSlotRes = StartTimeToTimeSlot(kvp.Key), // Key = TimeSlotId
                    Equipment = kvp.Value

                });
            }
            return reservations;
        }
        public static TimeSlot StartTimeToTimeSlot(int startTime) {
            return new TimeSlot {
                TimeSlotId = startTime - 7,
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

