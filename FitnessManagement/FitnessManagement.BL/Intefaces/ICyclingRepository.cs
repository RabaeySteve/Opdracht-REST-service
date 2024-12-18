using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface ICyclingRepository {

        IEnumerable<CyclingSession> GetAll();
        CyclingSession GetById(int id);
        bool IsCyclingSession(int id);
        List<CyclingSession> SessionsforMember(int memberId);
        public List<CyclingSession> GetByCustomerAndDate(int memberId, int year, int month);
        void AddSession(CyclingSession session);
       

    }
}
