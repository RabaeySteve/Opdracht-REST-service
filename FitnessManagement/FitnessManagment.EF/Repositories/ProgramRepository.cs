using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Mappers;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Repositories {
    public class ProgramRepository : IProgramRepository {
        private FitnessManagementContext ctx;

        public ProgramRepository(string connectionString) {
            this.ctx = new FitnessManagementContext(connectionString);
        }

        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public void AddProgram(Program program) {
            try {
                ctx.programs.Add(MapProgram.MapToDB(program));
                SaveAndClear();
            } catch (Exception ex) {
                throw new RepoException("ProgramRepo - AddProgram", ex);
            }
        }

        

        public IEnumerable<Program> GetAll() {
            try {
                return ctx.programs.Select(p => MapProgram.MapToDomain(p)).ToList();
            } catch (Exception ex) {
                throw new RepoException("ProgramRepo - GetAll", ex);
            }
        }

        

        public bool IsProgramNew(string name, DateTime startDate) {
            try {
                return ctx.programs.Any(p => p.Name == name && p.StartDate == startDate);
            } catch (Exception ex) {
                throw new RepoException("ProgramRepo - IsProgram", ex);
            }
        }

        public void UpdateProgram(Program program) {
            try {
                ctx.programs.Update(MapProgram.MapToDB(program));
                SaveAndClear();
            } catch (Exception ex) {
                throw new RepoException("ProgramRepo - UpdateProgram", ex);
            }
        }

        public bool IsProgram(string programCode) {
            try {
                return ctx.programs.Any(x => x.ProgramCode == programCode);
            } catch (Exception ex) {

                throw;
            }
        }

        public Program GetProgramByProgramCode(string programCode) {
            try {
                return MapProgram.MapToDomain(ctx.programs.Where(p => p.ProgramCode == programCode).AsNoTracking().FirstOrDefault());
            } catch (Exception ex) {

                throw new RepoException("ProgramRepo - GetProgramByProgramCode", ex);
            }
        }

        
    }
}
