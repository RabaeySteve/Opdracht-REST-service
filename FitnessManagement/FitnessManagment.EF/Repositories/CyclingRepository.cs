﻿using FitnessManagement.BL.Intefaces;
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
            } catch (Exception ex) {

                throw new RepoException("CyclingRepo - AddSession", ex);
            }
        }

       

        public IEnumerable<CyclingSession> GetAll() {
            throw new NotImplementedException();
        }

        public CyclingSession GetById(int id) {
            try {
                return MapCyclingSession.MapToDomain(ctx.cyclingsession.Where(c => c.CyclingSessionId == id).Include(x => x.Member).AsNoTracking().FirstOrDefault(), ctx);
            } catch (Exception ex) {

                throw new RepoException("CyclingRepo - GetById", ex);
            }
        }

        public bool IsCyclingSession(int id) {
            try {
                return ctx.cyclingsession.Any(c => c.CyclingSessionId == id);
            } catch (Exception ex) {

                throw new RepoException("CyclingRepo - IsCyclingSession", ex);
            }
        }

        public List<CyclingSession> SessionsforMember(int memberId) {
            try {
                List<CyclingSessionEF> cyclingSessionEFs = ctx.cyclingsession.Where(m => m.Member.MemberId == memberId)
                    .Include(m => m.Member).ToList();

                return cyclingSessionEFs.Select(c => MapCyclingSession.MapToDomain(c, ctx)).ToList();
            } catch (Exception ex) {

                throw new RepoException("CyclingRepo - SessionsforMember", ex);
            }
        }

        public List<CyclingSession> GetByCustomerAndDate(int memberId, int year, int month) {
            try {
                List<CyclingSessionEF> cyclingSessionsEF = ctx.cyclingsession
                   .Where(r => r.Member.MemberId == memberId &&
                               r.Date.Year == year &&
                               r.Date.Month == month)
                   .AsNoTracking()
                   .ToList();


                return cyclingSessionsEF.Select(x => MapCyclingSession.MapToDomain(x, ctx)).ToList();
            } catch (Exception ex) {
                throw new RepoException("Error fetching running sessions by customer and date", ex);
            }
        }
    }
}
