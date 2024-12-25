using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Exceptions;
using FitnessManagement.BL.Models;

using static FitnessBL.Models.Member;
using static FitnessManagement.BL.Models.Equipment;

namespace FitnessManagement.API.Mapper {
    public class MemberMapper {
        
        public static Member ToMember(MemberDTO dto) {
            try {
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
            } catch (Exception ex) {

                throw new MapperException("ToMember", ex);
            }
            
        }
        public static MemberDTO ToMemberDTO(Member m) {
            try {
                
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
            } catch (Exception ex) {

                throw new MapperException("ToMemberDTO", ex);
            }

        }

        public static MemberType MapStringToMembertype(string memberType) {
            if (!string.IsNullOrWhiteSpace(memberType)) {
                if (Enum.TryParse(memberType, out MemberType result)) {
                    return result;
                } else {
                    throw new MapperException($"Invalid Enum Membertype: {memberType}");
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
       
        
    }
}
