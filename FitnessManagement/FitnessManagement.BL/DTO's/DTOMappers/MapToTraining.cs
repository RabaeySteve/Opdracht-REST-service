using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.DTO_s.DTOMappers {
    public class MapToTraining {
        public static TrainingSession MapCyclingSessionToTraining(CyclingSession c) {
            try {
                return new TrainingSession {
                    Id = c.CyclingSessionId,
                    MemberId = c.CyclingMember.MemberId,
                    Date = DateOnly.FromDateTime(c.Date),
                    Duration = c.Duration,
                    TrainingType = "CyclingSession"
                };
            } catch (Exception) {

                throw;
            }
            
        }
        public static TrainingSession MapRunningSessionToTraining(RunningSession r) {
            try {
                return new TrainingSession {
                    Id = r.RunningSessionId,
                    MemberId = r.RunningMember.MemberId,
                    Date = DateOnly.FromDateTime(r.Date),
                    Duration = r.Duration,
                    TrainingType = "CyclingSession"
                };
            } catch (Exception) {

                throw;
            }

        }
    }
}
