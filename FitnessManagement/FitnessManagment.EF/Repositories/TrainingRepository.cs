
using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FitnessManagement.EF.Repositories {
    public class TrainingRepository : ITrainingRepository {
        private FitnessManagementContext ctx;

        public TrainingRepository(string connectioString) {
            this.ctx = new FitnessManagementContext(connectioString);
        }
        //Zet Cyclingsessions en RunningSessions samen in 1 list.
        public List<TrainingSessionBase> GetAllTrainingSessions(int memberId) {
            try {

                string connectionString = ctx.Database.GetDbConnection().ConnectionString;
                RunningRepository runningRepo = new RunningRepository(connectionString);
                CyclingRepository cyclingRepo = new CyclingRepository(connectionString);

                List<RunningSession> runningSessions = runningRepo.SessionsForMember(memberId);
                List<CyclingSession> cyclingSessions = cyclingRepo.SessionsforMember(memberId);


                List<TrainingSessionBase> allSessions = new List<TrainingSessionBase>();
                allSessions.AddRange(runningSessions);
                allSessions.AddRange(cyclingSessions);

                return allSessions;
            } catch (Exception ex) {
                throw new RepoException("TrainingRepo - GetAllTrainingSessions", ex);
            }
        }

        public CyclingSession GetCyclingDetails(int CylingSession) {
            try {
                string connectionString = ctx.Database.GetDbConnection().ConnectionString;
                CyclingRepository cyclingRepo = new CyclingRepository(connectionString);

                return cyclingRepo.GetById(CylingSession);
            } catch (Exception ex) {

                throw new RepoException("TrainingRepo - GetCyclingDetails", ex);
            }
        }
        public RunningSession GetRunningDetails(int runningSession) {
            try {
                string connectionString = ctx.Database.GetDbConnection().ConnectionString;
                RunningRepository runningRepo = new RunningRepository(connectionString);

                return runningRepo.GetById(runningSession);
            } catch (Exception ex) {

                throw new RepoException("TrainingRepo - GetRunningDetails", ex);
            }
        }



        public List<TrainingSessionBase> GetSessionsForCustomerMonth(int memberId, int year, int month) {
            try {
                var allSessions = GetAllTrainingSessions(memberId);

                return allSessions
                    .Where(x => x.Date.Year == year && x.Date.Month == month)
                    .OrderBy(session => session.Date)
                    .ToList();
            } catch (Exception ex) {
                throw new RepoException("TrainingRepo - GetCyclingDetails");
            }
        }



    }
}
