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
