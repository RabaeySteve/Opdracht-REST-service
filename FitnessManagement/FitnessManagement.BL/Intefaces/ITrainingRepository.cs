﻿
using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface ITrainingRepository {
        List<TrainingSession> GetAllTrainingSessions(int memberId);
        List<TrainingSession> GetSessionsForCustomerMonth(int memberId, int year, int month);

        TrainingStatistics GetStatisticsForCustomer(int memberId);

        RunningSession GetRunningDetails(int runningSession);
        CyclingSession GetCyclingDetails(int CylingSession);
    }
}
