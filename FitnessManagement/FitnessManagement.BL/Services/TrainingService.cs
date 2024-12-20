using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Services {
    public class TrainingService {
        private ITrainingRepository repo;

        public TrainingService(ITrainingRepository repo) {
            this.repo = repo;
        }
       

        public List<TrainingSession> GetMonthlySessionCountsType(int memberId, int year) {
            List<TrainingSession> sessions = repo.GetAllTrainingSessions(memberId);

            if (!sessions.Any(s => s.Date.Year == year)) {
                return new List<TrainingSession> { new TrainingSession() };
            }

            return sessions.Where(s => s.Date.Year == year).ToList();

        }

        public Dictionary<int, int> GetMonthlySessionCounts(int memberId, int year) {
            List<TrainingSession> sessions = repo.GetAllTrainingSessions(memberId);

            if (!sessions.Any(s => s.Date.Year == year)) {
                return new Dictionary<int, int>();
            }

            return sessions
                .Where(s => s.Date.Year == year)
                .GroupBy(s => s.Date.Month)
                .ToDictionary(g => g.Key, g => g.Count());
        }


        public TrainingStatistics GetTrainingStatistics(int memberId) {
            try {
                List<TrainingSession> sessions = repo.GetAllTrainingSessions(memberId);
                if (sessions.Count > 0) {
                    int totalSessions = sessions.Count;
                    int totalDurationInMinutes = 0;
                    int shortestSessionInMinutes = sessions.FirstOrDefault().Duration;
                    int longestSessionInMinutes = 0;

                    for (int i = 0; i < sessions.Count; i++) {
                        totalDurationInMinutes = totalDurationInMinutes + sessions[i].Duration;
                        if (shortestSessionInMinutes != 0) {
                            if (sessions[i].Duration < shortestSessionInMinutes) {
                                shortestSessionInMinutes = sessions[i].Duration;
                            }
                        }
                        if (sessions[i].Duration > longestSessionInMinutes) {
                            longestSessionInMinutes = sessions[i].Duration;
                        }

                    }
                    double averageSessionDurationInMinutes = totalDurationInMinutes / totalSessions;

                    return new TrainingStatistics {
                        TotalSessions = totalSessions,
                        TotalDurationInMinutes = totalDurationInMinutes,
                        AverageSessionDurationInMinutes = averageSessionDurationInMinutes,
                        ShortestSessionInMinutes = shortestSessionInMinutes,
                        LongestSessionInMinutes = longestSessionInMinutes
                    };
                }

                return null;

            } catch (Exception ex) {

                throw new Exception();
            }

        }
        public List<TrainingSession> GetSessionsForCustomerMonth(int memberId, int year, int month) {
            try {
                List<TrainingSession> sessions = repo.GetSessionsForCustomerMonth(memberId, year, month);
                return sessions.ToList();
            } catch (Exception) {

                throw;
            }

        }
        public ITrainingSession GetSessionDetails(int trainingId, string trainingType) {
            try {
                string TypeToLower = trainingType.ToLower();
                return TypeToLower switch {
                    "cycling" => GetCyclingDetails(trainingId),
                    "running" => GetRunningDetails(trainingId),
                    _ => throw new ArgumentException("Ongeldig trainingstype opgegeven.")
                };


            } catch (Exception ex) {

                throw;
            }
        }
        public CyclingSession GetCyclingDetails(int CylingSession) {
            try {
                return repo.GetCyclingDetails(CylingSession);
            } catch (Exception) {

                throw;
            }
        }
        public RunningSession GetRunningDetails(int runningSession) {
            try {
                return repo.GetRunningDetails(runningSession);
            } catch (Exception) {

                throw;
            }
        }
     
        public string EasyInputTrainingType(string trainingType) {
            return trainingType.ToLower() switch {
                "runningsession" => "RunningSession",
                "running" => "RunningSession",
                "cyclingsession" => "CyclingSession",
                "cycling" => "CyclingSession",
                _ => trainingType
            };
        }
    }
}
