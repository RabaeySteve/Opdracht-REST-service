using FitnessBL.Models;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitnessBL.Models.Member;
using static FitnessManagement.BL.Models.Program;

namespace FitnessManagement.EF.Mappers {
    public class MapProgram {
        public static Program MapToDomain(ProgramEF db) {
            try {
                return new Program(
                       db.ProgramCode,
                       db.Name,
                       db.StartDate,
                       MapStringToProgramTarget(db.Target),
                       db.MaxMembers

                    );
            } catch (Exception ex) {

                throw new MapException("MapProgram - MapToDomain", ex);
            }
        }
        public static ProgramEF MapToDB(Program p) {
            try {
                return new ProgramEF(
                       p.ProgramCode,
                       p.Name,
                       p.Target.ToString(),
                       p.StartDate,
                       p.MaxMembers


                    );
            } catch (Exception ex) {

                throw new MapException("MapProgram - MapToDB", ex);
            }
        }
        public static ProgramTarget MapStringToProgramTarget(string programTarget) {
            if (!string.IsNullOrWhiteSpace(programTarget)) {
                if (Enum.TryParse(programTarget, out ProgramTarget result)) {
                    return result;
                } else {
                    throw new MapException($"Invalid Enum ProgramTarget: {programTarget}");
                }
            } else {
                return ProgramTarget.noType;
            }


        }
    }
}
