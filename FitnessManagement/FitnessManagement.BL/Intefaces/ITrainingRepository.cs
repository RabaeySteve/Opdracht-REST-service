
using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface ITrainingRepository {
        List<TrainingSessionBase> GetAllTrainingSessions(int memberId);
        List<TrainingSessionBase> GetSessionsForCustomerMonth(int memberId, int year, int month);

        RunningSession GetRunningDetails(int runningSession);
        CyclingSession GetCyclingDetails(int CylingSession);
    }
}
