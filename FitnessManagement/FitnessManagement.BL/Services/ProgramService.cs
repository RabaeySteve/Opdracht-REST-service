using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Services {
    public class ProgramService {
        private IProgramRepository repo;

        public ProgramService(IProgramRepository repo) {
            this.repo = repo;
        }

        public IEnumerable<Program> GetAllPrograms() {
            try {
                return repo.GetAll();
            } catch (Exception ex) {
                throw new ProgramException("GetAllPrograms", ex);
            }
        }

        public Program AddProgram(Program program) {
            try {
                if (IsProgram(program.Name)) {
                    throw new ProgramException("Program already exists");
                } else {
                    repo.AddProgram(program);
                    return program;
                }
            } catch (Exception ex) {
                throw new ProgramException("AddProgram", ex);
            }
        }

        public Program UpdateProgram(Program program) {
            try {
                if (IsProgram(program.ProgramCode)) {
                    repo.UpdateProgram(program);
                    return program;
                } else {
                    throw new ProgramException("Program does not exist");
                }
            } catch (Exception ex) {
                throw new ProgramException("UpdateProgram", ex);
            }
        }

        public bool IsProgram(string programCode) {
            try {
                return repo.IsProgram(programCode);
            } catch (Exception ex) {
                throw new ProgramException("IsProgram", ex);
            }
        }
        public Program GetProgramByProgramCode(string programCode) {
            try {
                if (!repo.IsProgram(programCode)) {
                    throw new ProgramException("");
                }
                return repo.GetProgramByProgramCode(programCode);
            } catch (ProgramException ex) {

                throw new ProgramException("");
            }
        }


       


    }
}
