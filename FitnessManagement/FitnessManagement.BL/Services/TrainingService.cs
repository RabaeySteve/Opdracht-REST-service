using FitnessBL.Models;
using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Exceptions;
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

        public List<MonthlySessionImpact> GetTrainingStatisticsImpact(int memberId, int year) {
            try {
                List<TrainingSessionBase> sessions = repo.GetAllTrainingSessions(memberId);

                if (sessions.Count == 0) {
                    return new List<MonthlySessionImpact>();
                }

                var groupedSessions = sessions
                    .Where(s => s.Date.Year == year) // Filter op het specifieke jaar
                    .GroupBy(s => s.Date.Month) // Groepeer op maand (integerwaarde)
                    .OrderBy(g => g.Key); // Sorteer op maandnummer (integer)

                List<MonthlySessionImpact> monthlyOverview = new List<MonthlySessionImpact>();

                foreach (var group in groupedSessions) {
                    var cyclingFunSessions = group
                        .Where(s => s.TrainingSessionType == TrainingSessionType.Cycling && s is CyclingSession cycling && cycling.Type == CyclingSession.CyclingTrainingType.fun)
                        .ToList();

                    var cyclingEnduranceSessions = group
                        .Where(s => s.TrainingSessionType == TrainingSessionType.Cycling && s is CyclingSession cycling && cycling.Type == CyclingSession.CyclingTrainingType.endurance)
                        .ToList();

                    var cyclingIntervalSessions = group
                        .Where(s => s.TrainingSessionType == TrainingSessionType.Cycling && s is CyclingSession cycling && cycling.Type == CyclingSession.CyclingTrainingType.interval)
                        .ToList();

                    var cyclingRecoverySessions = group
                        .Where(s => s.TrainingSessionType == TrainingSessionType.Cycling && s is CyclingSession cycling && cycling.Type == CyclingSession.CyclingTrainingType.recovery)
                        .ToList();

                    var monthOverview = new MonthlySessionImpact {
                        Month = GetMonthName(group.Key),
                        RunningSessions = group.Count(s => s.TrainingSessionType == TrainingSessionType.Running),

                        // Cycling Sessions Fun
                        CyclingSessionsFun = cyclingFunSessions.Count,
                        FunImpactLow = cyclingFunSessions.Count(s => CalculateTrainingImpact(s) == "Low"),
                        FunImpactMedium = cyclingFunSessions.Count(s => CalculateTrainingImpact(s) == "Medium"),
                        FunImpactHigh = cyclingFunSessions.Count(s => CalculateTrainingImpact(s) == "High"),

                        // Cycling Sessions Endurance
                        CyclingSessionsEndurance = cyclingEnduranceSessions.Count,
                        EnduranceImpactLow = cyclingEnduranceSessions.Count(s => CalculateTrainingImpact(s) == "Low"),
                        EnduranceImpactMedium = cyclingEnduranceSessions.Count(s => CalculateTrainingImpact(s) == "Medium"),
                        EnduranceImpactHigh = cyclingEnduranceSessions.Count(s => CalculateTrainingImpact(s) == "High"),

                        // Cycling Sessions Interval
                        CyclingSessionsInterval = cyclingIntervalSessions.Count,
                        IntervalImpactLow = cyclingIntervalSessions.Count(s => CalculateTrainingImpact(s) == "Low"),
                        IntervalImpactMedium = cyclingIntervalSessions.Count(s => CalculateTrainingImpact(s) == "Medium"),
                        IntervalImpactHigh = cyclingIntervalSessions.Count(s => CalculateTrainingImpact(s) == "High"),

                        // Cycling Sessions Recovery
                        CyclingSessionsRecovery = cyclingRecoverySessions.Count,
                        RecoveryImpactLow = cyclingRecoverySessions.Count(s => CalculateTrainingImpact(s) == "Low"),
                        RecoveryImpactMedium = cyclingRecoverySessions.Count(s => CalculateTrainingImpact(s) == "Medium"),
                        RecoveryImpactHigh = cyclingRecoverySessions.Count(s => CalculateTrainingImpact(s) == "High")
                    };

                    monthlyOverview.Add(monthOverview);
                }

                return monthlyOverview.ToList();
            } catch (Exception ex) {
                throw new TrainingException("GetTrainingStatisticsImpact", ex);
            }
        }

        public string CalculateTrainingImpact(TrainingSessionBase session) {
            if (session is CyclingSession cyclingSession) {
                if (cyclingSession.AvgWatt < 150 && cyclingSession.Duration <= 90) return "Low";
                if (cyclingSession.AvgWatt < 150) return "Medium";
                if (cyclingSession.AvgWatt >= 150 && cyclingSession.AvgWatt <= 200) return "Medium";
                if (cyclingSession.AvgWatt > 200) return "High";
            }

            return "Unknown";
        }





        public List<MonthlySessionOverview> GetTrainingStatisticsPerMonth(int memberId, int year) {
            try {
                List<TrainingSessionBase> sessions = repo.GetAllTrainingSessions(memberId);

                if (sessions.Count == 0) {
                    return new List<MonthlySessionOverview>();
                }

                var groupedSessions = sessions
                 .Where(s => s.Date.Year == year) // Filter op het specifieke jaar
                 .GroupBy(s => s.Date.Month) // Groepeer op maand (integerwaarde)
                 .OrderBy(g => g.Key); // Sorteer op maandnummer (integer)



                List<MonthlySessionOverview> monthlyOverview = new List<MonthlySessionOverview>();

                foreach (var group in groupedSessions) {
                    var monthOverview = new MonthlySessionOverview {
                        Month = GetMonthName(group.Key), 
                        RunningSessions = group.Count(s => s.TrainingSessionType == TrainingSessionType.Running),
                        CyclingSessionsFun = group.Count(s =>
                            s.TrainingSessionType == TrainingSessionType.Cycling &&
                            s is CyclingSession cycling && cycling.Type == CyclingSession.CyclingTrainingType.fun),
                        CyclingSessionsEndurance = group.Count(s =>
                            s.TrainingSessionType == TrainingSessionType.Cycling &&
                            s is CyclingSession cycling && cycling.Type == CyclingSession.CyclingTrainingType.endurance),
                        CyclingSessionsInterval = group.Count(s =>
                            s.TrainingSessionType == TrainingSessionType.Cycling &&
                            s is CyclingSession cycling && cycling.Type == CyclingSession.CyclingTrainingType.interval),
                        CyclingSessionsRecovery = group.Count(s =>
                            s.TrainingSessionType == TrainingSessionType.Cycling &&
                            s is CyclingSession cycling && cycling.Type == CyclingSession.CyclingTrainingType.recovery)
                    };

                    monthlyOverview.Add(monthOverview);
                }

                return monthlyOverview.ToList();
            } catch (Exception ex) {
                throw new TrainingException("GetTrainingStatisticsPerMonth", ex);
            }
        }





        public TrainingStatistics GetTrainingStatistics(int memberId) {
            try {
                List<TrainingSessionBase> sessions = repo.GetAllTrainingSessions(memberId);

                if (sessions.Count == 0) {
                    return null;
                }

                int totalSessions = sessions.Count;
                int totalDurationInMinutes = sessions.Sum(s => s.Duration);
                int shortestSessionInMinutes = sessions.Min(s => s.Duration);
                int longestSessionInMinutes = sessions.Max(s => s.Duration);

                double averageSessionDurationInMinutes = totalDurationInMinutes / (double)totalSessions;

                return new TrainingStatistics {
                    TotalSessions = totalSessions,
                    TotalDurationInMinutes = totalDurationInMinutes,
                    AverageSessionDurationInMinutes = averageSessionDurationInMinutes,
                    ShortestSessionInMinutes = shortestSessionInMinutes,
                    LongestSessionInMinutes = longestSessionInMinutes
                };

            } catch (Exception ex) {

                throw new TrainingException("GetTrainingStatistics", ex);
            }

        }
        public List<TrainingSessionBase> GetSessionsForCustomerMonth(int memberId, int year, int month) {
            try {
                return repo.GetSessionsForCustomerMonth(memberId, year, month);
            } catch (Exception ex) {
                throw new TrainingException("GetSessionsForCustomerMonth", ex);
            }
        }

        public TrainingSessionBase GetSessionDetails(int trainingId, TrainingSessionType trainingType) {
            return trainingType switch {
                TrainingSessionType.Cycling => GetCyclingDetails(trainingId),
                TrainingSessionType.Running => GetRunningDetails(trainingId),
                _ => throw new ArgumentException("Invalid training type specified.")
            };
        }

        public CyclingSession GetCyclingDetails(int CylingSession) {
            try {
                return repo.GetCyclingDetails(CylingSession);
            } catch (Exception ex) {

                throw new TrainingException("GetSessionsForCustomerMonth", ex);
            }
        }
        public RunningSession GetRunningDetails(int runningSession) {
            try {
                return repo.GetRunningDetails(runningSession);
            } catch (Exception ex) {

                throw new TrainingException("GetRunningDetails", ex);
            }
        }
        private string GetMonthName(int month) {
    return month switch {
        1 => "Jan",
        2 => "Feb",
        3 => "Mar",
        4 => "Apr",
        5 => "May",
        6 => "Jun",
        7 => "Jul",
        8 => "Aug",
        9 => "Sep",
        10 => "Oct",
        11 => "Nov",
        12 => "Dec",
        _ => "Unknown"
    };
}

    }
}
