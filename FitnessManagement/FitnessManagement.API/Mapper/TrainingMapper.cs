using FitnessManagement.API.DTO_s;
using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.Mapper {
    public class TrainingMapper {
        public static TrainingImpact MapCyclingSessionToTraining(CyclingSession c) {
            try {
                return new TrainingImpact {
                    Id = c.TrainingId,
                    MemberId = c.CyclingMember.MemberId,
                    Date = DateOnly.FromDateTime(c.Date),
                    Duration = c.Duration,
                    TrainingType = "CyclingSession",
                    Impact = GetTrainingImpact(c)
                };
            } catch (Exception) {

                throw;
            }

        }
        public static TrainingTypeMonth MapTrainingTypeMonth(TrainingSession t) {
            try {
                return new TrainingTypeMonth {
                    TrainingType = t.TrainingType,
                    Date = t.Date,
                    Impact = t.GetType()


                };
            } catch (Exception) {

                throw;
            }

        }
        
    }
}
