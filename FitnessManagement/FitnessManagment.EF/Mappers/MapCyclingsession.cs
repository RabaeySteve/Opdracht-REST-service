﻿using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitnessBL.Models.Member;
using static FitnessManagement.BL.Models.CyclingSession;

namespace FitnessManagement.EF.Mappers {
    public class MapCyclingSession {
        public static CyclingSession MapToDomain(CyclingSessionEF db, FitnessManagementContext ctx) {
            try {
                return new CyclingSession(
                      db.CyclingSessionId,
                      db.Date,
                      db.Duration,
                      db.AvgWatt,
                      db.MaxWatt,
                      db.AvgCadence,
                      db.MaxCadence,
                      db.Comment,
                      MapMember.MapToDomain(db.Member, ctx),
                      MapStringToCycleType(db.TrainingType)
                      
                    );
            } catch (Exception ex) {

                throw new MapException("MapCyclingSession - MapToDomain", ex);
            }
        }
        public static CyclingSessionEF MapToDB(CyclingSession c, FitnessManagementContext ctx) {
            try {
                MemberEF memberEF = ctx.members.Find(c.Member.MemberId);
                if (memberEF == null) { memberEF = MapMember.MapToDB(c.Member); }
                return new CyclingSessionEF(
                      c.TrainingId,
                      c.Date,
                      c.Duration,
                      c.AvgWatt,
                      c.MaxWatt,
                      c.AvgWatt,
                      c.MaxCadence,
                      c.Type.ToString(),
                      c.Comment,
                      memberEF

                    ); ;
            } catch (Exception ex) {

                throw new MapException("MapCyclingSession - MapToDB");
            }
        }
        public static CyclingTrainingType MapStringToCycleType(string cyclingType) {
            if (!string.IsNullOrWhiteSpace(cyclingType)) {
                if (Enum.TryParse(cyclingType, out CyclingTrainingType result)) {
                    return result;
                } else {
                    throw new MapException($"Invalid Enum Membertype: {cyclingType}");
                }
            } else {
                return CyclingTrainingType.NoType;
            }


        }

    }
}
