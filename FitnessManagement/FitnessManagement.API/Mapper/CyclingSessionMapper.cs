﻿using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Exceptions;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.Mapper {
    public class CyclingSessionMapper {

        public static CyclingSession MapCyclingSession(CyclingSessionDTO c) {
           
            return new CyclingSession {
                TrainingId = c.CyclingSessionId,
                Member = IdToMember(c.MemberId),
                Date = c.Date,
                Duration = c.Duration,
                AvgWatt = c.AvgWatt,
                MaxCadence = c.MaxCadence,
                AvgCadence = c.AvgCadence,
                MaxWatt = c.MaxWatt,
                Comment = c.Comment,
                Type = c.Type


            };
        }
        public static CyclingSessionDTO MapCyclingSessionToDTO(CyclingSession c) {
           
                return new CyclingSessionDTO {
                    CyclingSessionId = c.TrainingId,
                    MemberId = c.Member.MemberId,
                    Date = c.Date,
                    Duration = c.Duration,
                    AvgWatt = c.AvgWatt,
                    MaxCadence = c.MaxCadence,
                    AvgCadence = c.AvgCadence,
                    MaxWatt = c.MaxWatt,
                    Comment = c.Comment,
                    Type = c.Type


                };
           
       
        }
        public static Member IdToMember(int memberId) {
            return new Member {
                MemberId = memberId
            };
        }
    }
}
