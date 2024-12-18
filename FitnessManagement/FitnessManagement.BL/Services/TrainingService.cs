using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Intefaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Services {
    public class TrainingService {
        private ITrainingRepository  repo;

        public TrainingService(ITrainingRepository repo) {
            this.repo = repo;
        }

        public List<TrainingSession> GetSessionsForCustomerMonth(int memberId, int year, int month) {
            try {
                List<TrainingSession> sessions = GetSessionsForCustomerMonth(memberId, year, month);
                return sessions.ToList();
            } catch (Exception) {

                throw;
            }
            
            //var runningSessions = _runningRepository.GetByCustomerAndDate(customerId, year, month);
            //var cyclingSessions = _cyclingRepository.GetByCustomerAndDate(customerId, year, month);

            //return runningSessions.Cast<TrainingSession>()
            //                      .Concat(cyclingSessions.Cast<TrainingSession>())
            //                      .OrderBy(session => session.Date);
        }

    }
}
