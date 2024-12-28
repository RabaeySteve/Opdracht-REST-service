using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClientWPF.Model {
    public class Reservation {
        public Reservation() {

        }
        public class ReservationPost {
            public int MemberId { get; set; }
            public DateOnly Date { get; set; }
            public List<TimeSlotEquipment> Reservations { get; set; }
        }
        public class TimeSlotEquipment {
            public int TimeSlotId { get; set; }
            public int EquipmentId { get; set; }
        }
        public class AvailableTimeSlots {
           public  Dictionary<int, List<Equipment>> AvailableTime {  get; set; }
        }
        public class TimeSlotEquipmentGetDTO {
            public TimeSlot TimeSlot { get; set; }
            public Equipment Equipment { get; set; }

        }
        public class ReservationGetDTO {
            public int ReservationId { get; set; }
            public int GroupsId { get; set; }
            public int MemberId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }

            public DateOnly Date { get; set; }
            public List<TimeSlotEquipmentGetDTO> Reservations { get; set; }
        }
        public class ReservationSetDTO {
            public ReservationSetDTO(int reservationId, int groupsId, int memberId, string firstName, string lastName, string email, DateOnly date, int timeSlotId, int startTime, int endTime, string partOfDay, int equipmentId, string equipmentType) {
                ReservationId = reservationId;
                GroupsId = groupsId;
                MemberId = memberId;
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                Date = date;
                TimeSlotId = timeSlotId;
                StartTime = startTime;
                EndTime = endTime;
                PartOfDay = partOfDay;
                EquipmentId = equipmentId;
                EquipmentType = equipmentType;
            }

            public int ReservationId { get; set; }
            public int GroupsId { get; set; }
            public int MemberId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }

            public DateOnly Date { get; set; }
            public int TimeSlotId { get; set; }
            public int StartTime { get; set; }
            public int EndTime { get; set; }
            public string PartOfDay { get; set; }
            public int EquipmentId { get; set; }
            public string EquipmentType { get; set; }
        }


    }
}
