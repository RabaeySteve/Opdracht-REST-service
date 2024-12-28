using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Repositories {
    public class TimeSlotRepository : ITimeSlotRepository {
        private readonly FitnessManagementContext ctx;

        public TimeSlotRepository(string connectionString) {
           
            ctx = new FitnessManagementContext(connectionString);
        }
    

    private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
        //was niet nodig maar ga het ook niet perse weg doen. 
        public void addTimeSlot(TimeSlot timeSlot) {
            try {
                ctx.time_slot.Add(MapTimeSlot.MapToDB(timeSlot));
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepoException("TimeSlotRepo - addTimeSlot", ex);
            }
        }

        public List<TimeSlot> GetAllTimeSlots() {
            try {
                return ctx.time_slot.Select(x => MapTimeSlot.MapToDomain(x)).ToList();
            } catch (Exception ex) {

                throw new RepoException("TimeSlotRepo - GetAllTimeSlots", ex);
            }
        }

        public TimeSlot GetTimeSlot(int startTime) {
            try {
                return MapTimeSlot.MapToDomain(ctx.time_slot.Where(x => x.StartTime == startTime).FirstOrDefault());
            } catch (Exception ex) {

                throw new RepoException("TimeSlotRepo - GetTimeSlot");
            }
        }
    }
}
