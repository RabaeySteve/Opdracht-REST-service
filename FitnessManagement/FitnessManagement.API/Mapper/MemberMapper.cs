using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.BL.Models;

using static FitnessBL.Models.Member;

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
                Interests = dto.Interests ?? new List<string>(),
                Type = MapStringToMembertype(dto.Type)
            };
        }

        public static MemberDTO ToMemberDTO(Member m) {
            return new MemberDTO {
                MemberId = m.MemberId,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Email = m.Email,
                Address = m.Address,
                Birthday = m.Birthday,
                Interests = m.Interests,
                Type = m.Type.ToString()
            };
        }

        public static MemberType MapStringToMembertype(string memberType) {
            if (!string.IsNullOrWhiteSpace(memberType)) {
                if (Enum.TryParse(memberType, out MemberType result)) {
                    return result;
                } else {
                    throw new ArgumentException($"Invalid Enum MemberType: {memberType}");
                }
            } else {
                return MemberType.noType;
            }
        }

        public static Member MapProgramsToMember(MemberProgramsDTO dto) {
            return new Member {
                MemberId = dto.MemberId,
                Programs = dto.ProgramsList
            };
        }
    }
}
