using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Exceptions;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Model;


namespace FitnessManagement.API.Mapper {
    public class ReservationMapper {
        public static ReservationGetDTO MapToGetDTO(Reservation r) {
            
                return new ReservationGetDTO {
                    ReservationId = r.ReservationId,
                    GroupsId = r.GroupsId,
                    Date = r.Date,
                    MemberId = r.Member.MemberId,
                    FirstName = r.Member.FirstName,
                    LastName = r.Member.LastName,
                    Email = r.Member.Email,
                    Reservations = r.TimeSLotEquipment.Select(x => new TimeSlotEquipmentGetDTO {
                        TimeSlot = x.Key != null ? IdToTimeSlot(x.Key) : null,
                        Equipment = new EquipmentString {
                            EquipmentId = x.Value.EquipmentId,
                            EquipmentType = x.Value.Type.ToString(), 
                            IsInMaintenance = x.Value.IsInMaintenance
                        }
                    }).ToList(),
                };
            
        }

        public static ReservationPutDTO MapToPutDTO(Reservation r) {
          
                return new ReservationPutDTO {
                    ReservationId = r.ReservationId,
                    GroupsId = r.GroupsId,
                    Date = r.Date,
                    MemberId = r.Member.MemberId,
                    Reservations = r.TimeSLotEquipment.Select(x => new TimeSlotEquipmentDTO {
                        TimeSlotId = x.Key,
                        EquipmentId = x.Value.EquipmentId
                    }).ToList(),

                };
            
        }
        public static Reservation MapDTOToReservation(ReservationPutDTO r) {
           
                return new Reservation {
                    ReservationId = r.ReservationId,
                    GroupsId = r.GroupsId,
                    Date = r.Date,
                    Member = IdToMember(r.MemberId),
                    TimeSLotEquipment = r.Reservations.ToDictionary(
                        r => r.TimeSlotId,
                        r => IdToEquipment(r.EquipmentId)
                        )
                   

                };
            
        }
        public static Reservation MapPostReservation(ReservationPostDTO r) {
			
                return new Reservation {
                    ReservationId = 0,
                    Date = r.Date,
                    Member = IdToMember(r.MemberId),
                    TimeSLotEquipment = r.Reservations.ToDictionary(
                        r => r.TimeSlotId,
                        r => IdToEquipment(r.EquipmentId)
                        ),
                    GroupsId = 0

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
        public static TimeSlot IdToTimeSlot(int TimeSlot) {
            return new TimeSlot(TimeSlot, TimeSlot + 7);
               
        }
    }



}

