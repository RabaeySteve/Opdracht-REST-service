using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Mappers;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessManagement.EF.Repositories {
    public class RunningRepository : IRunningSessionRepository {
        private FitnessManagementContext ctx;

        public RunningRepository(string connectionString) {
            this.ctx = new FitnessManagementContext(connectionString);
        }

        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public void AddSession(RunningSession session) {
            try {
                // Map de session naar een EF entity
                RunningSessionEF runningSessionEF = MapRunningSession.MapToDB(session, ctx);

                // Voeg de hoofdentiteit toe en sla op
                ctx.runningsession_main.Add(runningSessionEF);
                SaveAndClear(); // Slaat op en maakt de context schoon

                // Haal de toegewezen ID op
                int assignedId = runningSessionEF.RunningSessionId;

                // Wijs de ID toe aan de details
                foreach (RunningSessionDetailsEF detail in runningSessionEF.Details) {
                    detail.RunningSessionId = assignedId;
                    ctx.runningsession_detail.Add(detail);
                }

                // Sla de details op
                SaveAndClear();
            } catch (Exception ex) {
                throw new RepoException("Error adding running session", ex);
            }
        }


      

        public IEnumerable<RunningSession> GetAll() {

            try {
                List<RunningSessionEF> runningSessionEf = ctx.runningsession_main.AsNoTracking().ToList();

                return runningSessionEf.Select(r => MapRunningSession.MapToDomain(r, ctx)).ToList();
            } catch (Exception) {

                throw;
            }
           
        }

        public RunningSession GetById(int id) {
            try {
                RunningSessionEF runningSessionEF = ctx.runningsession_main
                        .Where(r => r.RunningSessionId == id)
                        .Include(x => x.Member)
                        .AsNoTracking()
                        .FirstOrDefault();


               return MapRunningSession.MapToDomain(runningSessionEF, ctx);

            } catch (Exception ex ) {
                throw new RepoException("GetById", ex);
            }
        }

        public bool IsRunningSession(int id) {
            try {
                return ctx.runningsession_main.Any(r => r.RunningSessionId == id);
            } catch (Exception) {
                throw;
            }
        }

        public List<RunningSession> SessionsForMember(int memberId) {
            try {
                List<RunningSessionEF> runningSessionsEF = ctx.runningsession_main
                    .Where(r => r.Member.MemberId == memberId)
                    .Include(r => r.Member)
                    .ToList();

                return runningSessionsEF
                    .Select(r => MapRunningSession.MapToDomain(r, ctx))
                    .ToList();
            } catch (Exception) {
                throw;
            }
        }

        public List<RunningSession> GetByCustomerAndDate(int memberId, int year, int month) {
            try {
                 List<RunningSessionEF> runningSessionsEF = ctx.runningsession_main
                    .Where(r => r.Member.MemberId == memberId &&
                                r.Date.Year == year &&
                                r.Date.Month == month)
                    .Include(r => r.Details) 
                    .AsNoTracking()
                    .ToList();

                
                return runningSessionsEF.Select(x => MapRunningSession.MapToDomain(x, ctx)).ToList();
            } catch (Exception ex) {
                throw new RepoException("Error fetching running sessions by customer and date", ex);
            }
        }
    }
}
