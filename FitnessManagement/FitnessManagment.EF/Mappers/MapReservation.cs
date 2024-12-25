using FitnessBL.Models;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using FitnessManagement.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessManagement.EF.Mappers {
    public class MapReservation {
        public static Reservation MapToDomain(ReservationEF db, FitnessManagementContext ctx) {
            try {
                
                List<ReservationEF> dbReservations = ctx.reservation.Where(r => r.GroupsId == db.GroupsId).Include(x => x.Equipment)
                    .Include(x => x.TimeSlot)
                    .Include(x => x.Member)
                    .AsNoTracking().ToList();

                if (!dbReservations.Any())
                    throw new MapException($"No reservations found for group ID {db.GroupsId}");

                // Haal de eerste reservatie op om basisinformatie te gebruiken
                var firstReservation = dbReservations.First();

                
                var reservation = new Reservation(
                    firstReservation.Date,
                    firstReservation.GroupsId,
                    firstReservation.ReservationId,
                    
                    MapMember.MapToDomain(firstReservation.Member, ctx)
                );

                // Voeg alle tijdslot- en toestelkoppelingen toe aan de Dictionary
                foreach (ReservationEF dbReservation in dbReservations) {
                    TimeSlot timeSlot = MapTimeSlot.MapToDomain(dbReservation.TimeSlot);
                    Equipment equipment = MapEquipment.MapToDomain(dbReservation.Equipment);

                    reservation.TimeSLotEquipment.Add(timeSlot.TimeSlotId, equipment);
                }

                return reservation;
            } catch (Exception ex) {
                throw new MapException("MapReservation - MapToDomain", ex);
            }
        }

        public static List<ReservationEF> MapToDB(Reservation reservation, FitnessManagementContext ctx) {
            try {
                if (reservation == null || !reservation.TimeSLotEquipment.Any()) {
                    throw new ArgumentException("De reservatie moet minstens één tijdslot bevatten.");
                }

                List<ReservationEF> reservationEFs = new List<ReservationEF>();
                int counter = 0;

                foreach (KeyValuePair<int, Equipment> timeSlotEquip in reservation.TimeSLotEquipment) {
                    int timeSlotId = timeSlotEquip.Key;
                    Equipment equipment = timeSlotEquip.Value;

                    TimeSlotEF timeSlotEF = ctx.time_slot.Find(timeSlotId);
                    if (timeSlotEF == null) {
                        throw new MapException($"TimeSlot with ID {timeSlotId} does not exist.");
                    }

                    
                    EquipmentEF equipmentEF = ctx.equipment.Find(equipment.EquipmentId);
                    if (equipmentEF == null) {
                        equipmentEF = MapEquipment.MapToDB(equipment);
                    }

                   
                    MemberEF memberEF = ctx.members.Find(reservation.Member.MemberId);
                    if (memberEF == null) {
                        memberEF = MapMember.MapToDB(reservation.Member);
                    }

                    
                    ReservationEF reservationEF = new ReservationEF(
                        reservation.ReservationId + counter, // Genereer uniek ReservationId
                        reservation.Date,
                        equipmentEF,
                        timeSlotEF,
                        memberEF,
                        reservation.GroupsId // Houd dezelfde groeps-ID
                    );

                   
                    reservationEFs.Add(reservationEF);
                    counter++;
                }

                return reservationEFs;
            } catch (Exception ex) {
                throw new MapException("MapToDB - Error while mapping Reservations to DB.", ex);
            }
        }
        public static Reservation MapToDomainMaintenaceUpdate(ReservationEF db, FitnessManagementContext ctx) {
            try {
                Dictionary<int, Equipment> timeSlotEquipment = new Dictionary<int, Equipment> {
                    {db.TimeSlot.TimeSlotId, MapEquipment.MapToDomain(db.Equipment)}
                };


                return new Reservation {
                    Date = db.Date,
                    ReservationId = db.ReservationId,
                    GroupsId = db.GroupsId,
                    Member = MapMember.MapToDomain(db.Member, ctx),
                    TimeSLotEquipment = timeSlotEquipment

                };
                
                
            } catch (Exception ex) {
                throw new MapException("MapReservation - MapToDomain", ex);
            }
        }



    }
}
