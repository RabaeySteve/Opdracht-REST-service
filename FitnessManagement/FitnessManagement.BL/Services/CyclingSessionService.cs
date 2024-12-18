using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace FitnessManagement.BL.Services {
    public class CyclingSessionService {
        private ICyclingRepository repo;

        public CyclingSessionService(ICyclingRepository repo) {
            this.repo = repo;
        }

        public bool IsCyclingSession(int id) {
            try {
                return repo.IsCyclingSession(id);
            } catch (Exception) {

                throw;
            }
        }

        public CyclingSession GetById(int id) {
            try {
                if (!repo.IsCyclingSession(id)) throw new CyclingSessionException("");
                return repo.GetById(id);
            } catch (Exception) {

                throw;
            }
        }

        public string TrainingImpact(CyclingSession session) {
            if (session.AvgWatt < 150 && session.Duration <= 90) return "low";
            if (session.AvgWatt < 150 && session.Duration > 90) return "medium";
            if (session.AvgWatt >= 150 && session.AvgWatt <= 200) return "medium";
            if (session.AvgWatt > 200) return "high";
            return "unknown";
        }

        public CyclingSession AddSession(CyclingSession session) {
            try {
                if (repo.IsCyclingSession(session.CyclingSessionId)) throw new CyclingSessionException("");
                repo.AddSession(session);

                return session;
            } catch (Exception) {

                throw;
            }
            
            
        }
        
       
        public List<CyclingSession> SessionsforMember(int memberId) {
            try {
                return repo.SessionsforMember(memberId);
            } catch (Exception) {

                throw;
            }
        }
        public List<CyclingSession> GetByCustomerAndDate(int memberId, int year, int month) {
            try {
                return repo.GetByCustomerAndDate(memberId, year, month);
            } catch (Exception ex) {

                throw;
            }
        }

    }
}
