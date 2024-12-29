using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;

namespace FitnessManagement.BL.Services {
    public class ProgramService {
        private readonly IProgramRepository repo;

        public ProgramService(IProgramRepository repo) {
            this.repo = repo;
        }

        public IEnumerable<Program> GetAllPrograms() {
            try {
                return repo.GetAll();
            } catch (Exception ex) {
                throw new ProgramException("GetAllPrograms - Unexpected error occurred", ex);
            }
        }

        public Program AddProgram(Program program) {
            try {
                if (IsProgram(program.Name)) {
                    throw new ProgramException("AddProgram - Program already exists");
                }

                repo.AddProgram(program);
                return program;
            } catch (ProgramException) {
                throw;
            } catch (Exception ex) {
                throw new ProgramException("AddProgram - Unexpected error occurred", ex);
            }
        }

        public Program UpdateProgram(Program program) {
            try {
                if (IsProgram(program.ProgramCode)) {
                    repo.UpdateProgram(program);
                    return program;
                }

                throw new ProgramException("UpdateProgram - Program does not exist");
            } catch (ProgramException) {
                throw;
            } catch (Exception ex) {
                throw new ProgramException("UpdateProgram - Unexpected error occurred", ex);
            }
        }

        public bool IsProgram(string programCode) {
            try {
                return repo.IsProgram(programCode);
            } catch (ProgramException) {
                throw;
            } catch (Exception ex) {
                throw new ProgramException("IsProgram - Unexpected error occurred", ex);
            }
        }

        public Program GetProgramByProgramCode(string programCode) {
            try {
                if (!repo.IsProgram(programCode)) {
                    throw new ProgramException("GetProgramByProgramCode - Program does not exist");
                }

                return repo.GetProgramByProgramCode(programCode);
            } catch (ProgramException) {
                throw;
            } catch (Exception ex) {
                throw new ProgramException("GetProgramByProgramCode - Unexpected error occurred", ex);
            }
        }
    }
}
