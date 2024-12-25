using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Exceptions;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Model;


namespace FitnessManagement.API.Mapper {
    public class ReservationMapper {
        public static ReservationGetDTO MapToGetDTO(Reservation r) {
            try {
                return new ReservationGetDTO {
                    ReservationId = r.ReservationId,
                    GroupsId = r.GroupsId,
                    Date = r.Date,
                    MemberId = r.Member.MemberId,
                    Reservations = r.TimeSLotEquipment.Select(x => new TimeSlotEquipmentGetDTO {
                        TimeSlot = x.Key != null? IdToTimeSlot(x.Key) : null,
                        Equipment = x.Value ,
                    }).ToList(),

                };
            } catch (Exception ex) {

                throw new MapperException("MapToGetDTO", ex);
            }
        }
        public static ReservationPutDTO MapToPutDTO(Reservation r) {
            try {
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
            } catch (Exception ex) {

                throw new MapperException("MapToPutDTO", ex);
            }
        }
        public static Reservation MapDTOToReservation(ReservationPutDTO r) {
            try {
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
            } catch (Exception ex) {

                throw new MapperException("MapDTOToReservation", ex);
            }
        }
        public static Reservation MapPostReservation(ReservationPostDTO r) {
			try {
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
			} catch (Exception ex) {

                throw new MapperException("MapPostReservation", ex);
            }
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

