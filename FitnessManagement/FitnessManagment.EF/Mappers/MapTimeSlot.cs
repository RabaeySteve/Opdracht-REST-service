using FitnessBL.Models;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Mappers {
    public class MapTimeSlot {
        public static TimeSlot MapToDomain(TimeSlotEF db) {
            try {
                return new TimeSlot(
                      db.TimeSlotId,
                      db.StartTime
                      
                    );
            } catch (Exception ex) {

                throw new MapException("MapTimeSlot - MapToDomain", ex);
            }
        }
        public static TimeSlotEF MapToDB(TimeSlot t) {
            try {
                return new TimeSlotEF(
                    t.TimeSlotId,
                    t.StartTime,
                    t.EndTime,
                    t.PartOfDay
                       
                    );
            } catch (Exception ex) {

                throw new MapException("MapTimeSlot - MapToDB");
            }
        }
    }
}
