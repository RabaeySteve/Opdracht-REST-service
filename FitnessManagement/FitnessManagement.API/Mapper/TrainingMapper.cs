using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Exceptions;
using FitnessManagement.BL.DTO_s.DTOModels;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.Mapper {
    public class TrainingMapper {

        public static TrainingMapped MapTraining(TrainingSessionBase t) {

            return new TrainingMapped {
                TrainingId = t.TrainingId,
                MemberId = t.Member.MemberId,
                Date = t.Date,
                Duration = t.Duration,
                TrainingSessionType = t.TrainingSessionType,

            };

        }

    }
}
