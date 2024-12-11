using FitnessBL.Models;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitnessBL.Models.Member;
using static FitnessManagement.EF.Mappers.MapProgram;
using static FitnessManagement.EF.Repositories.MemberRepositoryEF;
namespace FitnessManagement.EF.Mappers {
    public class MapMember {
        public static Member MapToDomain(MemberEF db, FitnessManagementContext ctx) {
			try {
                
                return new Member(
                       db.MemberId,
                       db.FirstName,
                       db.LastName,
                       db.Email,
                       db.Address,
                       db.Birthday,
                       MapInterestStringToList(db.Interests), 
                       MapStringToMembertype(db.Type),
                       GetProgramList(db.MemberId, ctx)

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
                        m.Type.ToString(),
                        MapListToMemberPrograms(m.Programs, m.MemberId)

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

        //public static List<Program> MapMemberProgramsToList() {
        //    if (programMember == null ) {
        //        return new List<Program>();
        //    }

        //    return programMember.Select(pm => new Program {
        //        ProgramCode = pm.ProgramCode,
        //        Name = pm.Program.Name,
        //        StartDate = pm.Program.StartDate,
        //        Target = MapStringToProgramTarget(pm.Program.Target),
        //        MaxMembers = pm.Program.MaxMembers
        //    }).ToList();
        //}

        public static List<ProgramMember> MapListToMemberPrograms(List<Program> programs, int memberId) {
            return programs.Select(p => new ProgramMember {
                MemberId = memberId,
                ProgramCode = p.ProgramCode,
                Program = new ProgramEF {
                    ProgramCode = p.ProgramCode,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    Target = p.Target.ToString(),
                    MaxMembers = p.MaxMembers
                }
            }).ToList();
        }

        public static List<Program> GetProgramList(int memberId, FitnessManagementContext ctx) {
            try {
                
                List<ProgramEF> programEFs = ctx.programMember
                    .Include(pm => pm.Program)
                    .Where(pm => pm.MemberId == memberId)
                    .Select(pm => pm.Program)
                    .ToList();
                if (programEFs.Count == 0) return new List<Program>();
                List<Program> result = new List<Program>();
                foreach (ProgramEF programEF in programEFs) {
                    Program mapProgram = MapProgram.MapToDomain(programEF);
                    result.Add(mapProgram);
                }
                return result;



            } catch (Exception ex) {

                throw;
            }
        }


    }
}
