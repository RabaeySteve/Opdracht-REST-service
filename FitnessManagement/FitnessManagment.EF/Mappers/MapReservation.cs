using FitnessBL.Models;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using FitnessManagement.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Mappers {
    public class MapReservation {

        public static Reservation MapToDomain(ReservationEF db, FitnessManagementContext ctx) {
            try {
                return new Reservation(
                      db.Date,
                      db.ReservationId,
                      MapEquipment.MapToDomain(db.Equipment),
                      MapTimeSlot.MapToDomain(db.TimeSlot),
                      MapMember.MapToDomain(db.Member, ctx),
                      db.GroupsId
                    );
            } catch (Exception ex) {

                throw new MapException("MapReservation - MapToDomain", ex);
            }
        }
        public static ReservationEF MapToDB(Reservation r, FitnessManagementContext ctx) {
            try {
                EquipmentEF equipmentEF = ctx.equipment.Find(r.Equipment.EquipmentId);
                if (equipmentEF == null) { equipmentEF = MapEquipment.MapToDB(r.Equipment); }
                TimeSlotEF timeSlotEF = ctx.time_slot.Find(r.TimeSlotRes.TimeSlotId);
                if (timeSlotEF == null) { timeSlotEF = MapTimeSlot.MapToDB(r.TimeSlotRes); }
                MemberEF memberEF = ctx.members.Find(r.Member.MemberId);
                if (memberEF == null) { memberEF = MapMember.MapToDB(r.Member); }
                return new ReservationEF(
                      r.ReservationId,
                      r.Date,
                      equipmentEF,
                      timeSlotEF,
                      memberEF,
                      r.GroepsId
                    );
            } catch (Exception ex) {

                throw new MapException("MapReservation - MapToDB");
            }
        }

       
    }
}
