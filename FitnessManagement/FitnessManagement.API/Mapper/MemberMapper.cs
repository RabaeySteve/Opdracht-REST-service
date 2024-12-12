using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.EF.Exceptions;
using static FitnessBL.Models.Member;

namespace FitnessManagement.API.Mapper {
    public class MemberMapper {
        public static AddMemberDTO AddMemberPapper(Member member) {
            return new AddMemberDTO {
                MemberId = member.MemberId,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                Address = member.Address,
                Birthday = member.Birthday,
                Interests = member.Interests

            };
        }
        public static Member ToMember(AddMemberDTO dto) {
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
    }
}
