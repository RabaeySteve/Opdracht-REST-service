﻿using FitnessManagement.BL.Models;
using FitnessManagement.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Mappers {
    public class MapRunningSession {
        public static RunningSession MapToDomain(RunningSessionEF db, FitnessManagementContext ctx) {
            try {
                return new RunningSession(
               db.RunningSessionId,
               MapMember.MapToDomain(db.Member, ctx),
               db.Date,
               db.Duration,
               db.AvgSpeed,
               GetDetailList(db.RunningSessionId, ctx)
           );
            } catch (Exception ex) {

                throw;
            }
           
        }
        public static RunningSessionEF MapToDB(RunningSession r,  FitnessManagementContext ctx) {
            MemberEF memberEF = ctx.members.Find(r.Member.MemberId);
                if (memberEF == null) { memberEF = MapMember.MapToDB(r.Member); }

            try {
                return new RunningSessionEF(
                    r.TrainingId,
                    r.Date,
                    r.Duration,
                    r.AvgSpeed,
                    memberEF,
                    MapDetailsToDB(r.Details)

                );
            } catch (Exception ex) {

                throw;
            }
            
        }


        public static List<RunningSessionDetail> GetDetailList(int runningSessionId, FitnessManagementContext ctx) {
            try {
                List<RunningSessionDetailsEF> runningSessionDetailsEFs = ctx.runningsession_detail
                    .Where(rd => rd.RunningSessionId == runningSessionId)
                    .ToList();

                if (runningSessionDetailsEFs.Count == 0) {
                    return new List<RunningSessionDetail>();
                }
                List<RunningSessionDetail> results = new List<RunningSessionDetail>();
                foreach(RunningSessionDetailsEF runningSessionDetailsEF in runningSessionDetailsEFs) {
                    RunningSessionDetail runningSessionDetail = MapRunningSessionDetail.MapToDomain(runningSessionDetailsEF);
                    results.Add(runningSessionDetail);
                }
                return results;
            } catch (Exception) {

                throw;
            }
        }
        public static List<RunningSessionDetailsEF> MapDetailsToDB(List<RunningSessionDetail> details) {
            try {
                 List<RunningSessionDetailsEF> detailsEF = new List<RunningSessionDetailsEF>();
                foreach (RunningSessionDetail detail in details) {

                    RunningSessionDetailsEF detailEF = MapRunningSessionDetail.MapToDB(detail);
                    detailsEF.Add(detailEF);
                }
                return detailsEF;
            } catch (Exception) {

                throw;
            }
        }

    }
}
