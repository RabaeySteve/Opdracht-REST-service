using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Exceptions;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.Mapper {
    public class RunningSessionMapper {
        public static RunningSession MapRunningSession(RunningSessionDTO r) {

            return new RunningSession {
                TrainingId = r.RunningSessionId,
                Member = IdToMember(r.MemberId),
                Date = r.Date,
                Duration = r.Duration,
                AvgSpeed = r.AvgSpeed,
                Details = r.Details.Select(d => new RunningSessionDetail {
                    RunningSessionId = r.RunningSessionId,
                    SeqNr = d.SeqNr,
                    IntervalTime = d.IntervalTime,
                    IntervalSpeed = d.IntervalSpeed
                }).ToList()
            };

        }


        public static RunningSessionDTO MapToRunningDTO(RunningSession s) {

            return new RunningSessionDTO {
                RunningSessionId = s.TrainingId,
                MemberId = s.Member.MemberId,
                Date = s.Date,
                Duration = s.Duration,
                AvgSpeed = s.AvgSpeed,
                Details = s.Details.Select(d => new RunningSessionDetailDTO {
                    SeqNr = d.SeqNr,
                    IntervalTime = d.IntervalTime,
                    IntervalSpeed = d.IntervalSpeed
                }).ToList()
            };


        }

        public static Member IdToMember(int memberId) {
            return new Member {
                MemberId = memberId
            };
        }
    }
}
