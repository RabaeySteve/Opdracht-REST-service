using FitnessBL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitnessBL.Models.Member;

namespace FitnessManagement.EF.Mappers {
    public class MapMember {
        public static Member MapToDomain(MemberEF db) {
			try {

                return new Member(
                       db.MemberId,
                       db.FirstName,
                       db.LastName,
                       db.Email,
                       db.Address,
                       db.Birthday,
                       MapInterestStringToList(db.Interests), 
                       MapStringToMembertype(db.Type)
                       
                    );
			} catch (Exception ex) {

				throw new MapException("MapMember - MapToDomain", ex);
			}
        }
        public static MemberEF MapToDB(Member m) {
            try {
                return new MemberEF(
                        m.MemberId,
                        m.FirstName,
                        m.LastName,
                        m.Email,
                        m.Address,
                        m.Birthday,
                        MapInterestToString(m.Interests),
                        m.Type.ToString()

                    ) ;
            } catch (Exception ex) {

                throw new MapException("MapMember - MapToDB");
            }
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
        public static List<string> MapInterestStringToList(string interest) {
            return !string.IsNullOrWhiteSpace(interest) ? new List<string>(interest.Split(',')) : new List<string>();
        }

        public static string MapInterestToString(List<string> interest) {
            return interest != null ? string.Join(", ", interest) : string.Empty;
        }
    }
}
