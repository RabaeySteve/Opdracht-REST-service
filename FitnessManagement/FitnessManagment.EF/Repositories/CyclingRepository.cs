using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Mappers;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Repositories {
    public class CyclingRepository : ICyclingRepository{
        private FitnessManagementContext ctx;

        public CyclingRepository(string connectioString) {
            this.ctx = new FitnessManagementContext(connectioString);
        }
        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public void AddSession(CyclingSession session) {
            try {
                ctx.cyclingsession.Add(MapCyclingSession.MapToDB(session, ctx));
                SaveAndClear();
            } catch (Exception) {

                throw;
            }
        }

        public void Delete(int sessionid) {
            CyclingSessionEF cyclingSessionEF = ctx.cyclingsession.Where(c => c.CyclingSessionId == sessionid).FirstOrDefault();
            if (cyclingSessionEF != null) {
                ctx.cyclingsession.Remove(cyclingSessionEF);
                SaveAndClear();
            }
        }

        public IEnumerable<CyclingSession> GetAll() {
            throw new NotImplementedException();
        }

        public CyclingSession GetById(int id) {
            try {
                return MapCyclingSession.MapToDomain(ctx.cyclingsession.Where(c => c.CyclingSessionId == id).Include(x => x.Member).AsNoTracking().FirstOrDefault(), ctx);
            } catch (Exception) {

                throw;
            }
        }

        public bool IsCyclingSession(int id) {
            try {
                return ctx.cyclingsession.Any(c => c.CyclingSessionId == id);
            } catch (Exception) {

                throw;
            }
        }

        public List<CyclingSession> SessionsforMember(int memberId) {
            try {
                List<CyclingSessionEF> cyclingSessionEFs = ctx.cyclingsession.Where(m => m.Member.MemberId == memberId)
                    .Include(m => m.Member).ToList();

                return cyclingSessionEFs.Select(c => MapCyclingSession.MapToDomain(c, ctx)).ToList();
            } catch (Exception) {

                throw;
            }
        }

        public void UpdateSession(CyclingSession session) {
            try {
                CyclingSessionEF sessionEF = MapCyclingSession.MapToDB(session, ctx);
                ctx.cyclingsession.Update(sessionEF);
                SaveAndClear();
            } catch (Exception) {

                throw;
            }
        }
    }
}
