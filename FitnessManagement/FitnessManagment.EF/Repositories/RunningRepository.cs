using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
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
                RunningSessionEF runninSessionEF = MapRunningSession.MapToDB(session, ctx);
                ctx.RunningSession_main.Add(runninSessionEF);
                SaveAndClear();
            } catch (Exception) {
                throw;
            }
        }

        public void Delete(int sessionId) {
            RunningSessionEF runningSessionEF = ctx.RunningSession_main
                .Where(r => r.RunningSessionId == sessionId)
                .FirstOrDefault();

            if (runningSessionEF != null) {
                ctx.RunningSession_main.Remove(runningSessionEF);
                SaveAndClear();
            }
        }

        public IEnumerable<RunningSession> GetAll() {

            try {
                List<RunningSessionEF> runningSessionEf = ctx.RunningSession_main.AsNoTracking().ToList();

                return runningSessionEf.Select(r => MapRunningSession.MapToDomain(r, ctx)).ToList();
            } catch (Exception) {

                throw;
            }
           
        }

        public RunningSession GetById(int id) {
            try {
                return MapRunningSession.MapToDomain(
                    ctx.RunningSession_main
                        .Where(r => r.RunningSessionId == id)
                        .Include(x => x.Member)
                        .AsNoTracking()
                        .FirstOrDefault(), ctx);
            } catch (Exception) {
                throw;
            }
        }

        public bool IsRunningSession(int id) {
            try {
                return ctx.RunningSession_main.Any(r => r.RunningSessionId == id);
            } catch (Exception) {
                throw;
            }
        }

        public List<RunningSession> SessionsForMember(int memberId) {
            try {
                List<RunningSessionEF> runningSessionsEF = ctx.RunningSession_main
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

        public void UpdateSession(RunningSession session) {
            try {
                ctx.RunningSession_main.Update(MapRunningSession.MapToDB(session, ctx));
                SaveAndClear();
            } catch (Exception) {
                throw;
            }
        }
    }
}
