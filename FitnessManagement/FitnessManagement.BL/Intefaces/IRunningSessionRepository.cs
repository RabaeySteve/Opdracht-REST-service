using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface IRunningSessionRepository {
        IEnumerable<RunningSession> GetAll();
        RunningSession GetById(int id);
        bool IsRunningSession(int id);
        List<RunningSession> SessionsForMember(int memberId);

        void AddSession(RunningSession session);
        void UpdateSession(RunningSession session);
        void Delete(int sessionId);
    }

}
