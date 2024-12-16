using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Mappers {
    public class MapRunningSessionDetail {
        public static RunningSessionDetail MapToDomain(RunningSessionDetailsEF db) {
            try {
                return new RunningSessionDetail(
                     db.RunningSessionId,
                     db.SeqNr,
                     db.IntervalTime,
                     db.IntervalSpeed
                    );
            } catch (Exception ex) {

                throw new MapException("MapCyclingSession - MapToDomain", ex);
            }
        }
        public static RunningSessionDetailsEF MapToDB(RunningSessionDetail d) {
            try {
                return new RunningSessionDetailsEF (
                     d.RunningSessionId,
                     d.SeqNr,
                     d.IntervalTime,
                     d.IntervalSpeed
                );
               
                    
            } catch (Exception ex) {

                throw new MapException("MapCyclingSession - MapToDB");
            }
        }
    }
}
