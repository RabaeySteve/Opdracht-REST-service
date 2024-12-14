using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
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

        public void AddSession(CyclingSession session) {
            throw new NotImplementedException();
        }

        public void Delete(int sessionid) {
            throw new NotImplementedException();
        }

        public IEnumerable<CyclingSession> GetAll() {
            throw new NotImplementedException();
        }

        public CyclingSession GetById(int id) {
            throw new NotImplementedException();
        }

        public bool IsCyclingSession(int id) {
            throw new NotImplementedException();
        }

        public List<CyclingSession> SessionsforMember(int memberId) {
            throw new NotImplementedException();
        }

        public void UpdateSession(CyclingSession session) {
            throw new NotImplementedException();
        }
    }
}
