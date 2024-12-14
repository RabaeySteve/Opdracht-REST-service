using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using static FitnessBL.Models.Member;
using static FitnessManagement.BL.Models.Equipment;

namespace FitnessManagement.API.Mapper {
    public class MemberMapper {
        
        public static Member ToMember(MemberDTO dto) {
            return new Member {
                MemberId = dto.MemberId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Birthday = dto.Birthday,
                Interests = dto.Interests,
                Type = MapStringToMembertype(dto.Type)
            };
        }

        public static MemberType MapStringToMembertype(string memberType) {
            if (!string.IsNullOrWhiteSpace(memberType)) {
                if (Enum.TryParse(memberType, out MemberType result)) {
                    return result;
                } else {
                    throw new MapException($"Invalid Enum Membertype: {memberType}");
                }
            } else {
                return MemberType.noType;
            }


        }

        public static Member MapProgramsToMember(MemberProgramsDTO dto) {
            return new Member() {
                MemberId=dto.MemberId,
                Programs = dto.ProgramsList
            };
        }
        public static MemberReservationsDTO MapReservationsToMember(List<Reservation> reservations) {
            return new MemberReservationsDTO() {
                MemberId = MemberIdFromReservations(reservations),
                ReservationsList = reservations.Select(r => new MemberReservationsListDTO {
                    ReservationId = r.ReservationId,
                    Date = r.Date,
                    StartTime = r.TimeSlotRes.StartTime,
                    equipmentType = r.Equipment.Type
                }).ToList()
            };

        }

        public static int MemberIdFromReservations(List<Reservation> reservations) {
            try {
                Reservation reservation = reservations.FirstOrDefault();
                int reservationMember = reservation.Member.MemberId;
                return reservationMember;
            } catch (Exception) {

                throw;
            }
        }
    }
}
