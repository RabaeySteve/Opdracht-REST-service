using FitnessManagement.BL.DTO_s.DTOMappers;
using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FitnessManagement.EF.Repositories {
    public class TrainingRepository : ITrainingRepository
    {
        private FitnessManagementContext ctx;

        public TrainingRepository(string connectioString) {
            this.ctx = new FitnessManagementContext(connectioString);
        }

        public List<TrainingSession> GetAllTrainingSessions(int memberId) {
            try {
                string connectionString = ctx.Database.GetDbConnection().ConnectionString;
                var runningRepo = new RunningRepository(connectionString);
                var cyclingRepo = new CyclingRepository(connectionString);
                List<RunningSession> runningSessions = runningRepo.SessionsForMember(memberId);
                List<CyclingSession> cyclingSessions = cyclingRepo.SessionsforMember(memberId);

                List<TrainingSession> trainingSessionsCycling = cyclingSessions.Select(x => MapToTraining.MapCyclingSessionToTraining(x)).ToList();
                List<TrainingSession> trainingSessionsRunning = runningSessions.Select(x => MapToTraining.MapRunningSessionToTraining(x)).ToList();

                List<TrainingSession> allSessions = new List<TrainingSession>();


                allSessions.AddRange(trainingSessionsCycling);
                allSessions.AddRange(trainingSessionsRunning);


                return allSessions;


            } catch (Exception) {

                throw;
            }
        }
        public CyclingSession GetCyclingDetails(int CylingSession) {
            try {
                string connectionString = ctx.Database.GetDbConnection().ConnectionString;
                var cyclingRepo = new CyclingRepository(connectionString);

                return cyclingRepo.GetById(CylingSession);
            } catch (Exception) {

                throw;
            }
        }
        public RunningSession GetRunningDetails(int runningSession) {
            try {
                string connectionString = ctx.Database.GetDbConnection().ConnectionString;
                var runningRepo = new RunningRepository(connectionString);

                return runningRepo.GetById(runningSession);
            } catch (Exception) {

                throw;
            }
        }

    

        public List<TrainingSession> GetSessionsForCustomerMonth(int memberId, int year, int month) {
            try {
                List<TrainingSession> allSessions = GetAllTrainingSessions(memberId);



                return allSessions.Where(x => x.Date.Year == year && x.Date.Month == month)
                    .OrderBy(session => session.Date).ToList();


            } catch (Exception) {

                throw;
             

            }
        }

        public TrainingStatistics GetStatisticsForCustomer(int customerId) {
            throw new NotImplementedException();
        }
    }
}
