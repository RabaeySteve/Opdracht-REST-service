using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Mappers;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Repositories {
    public class ReservationRepository : IReservationRepository {
        private FitnessManagementContext ctx;

        public ReservationRepository(string connectionString) {
            this.ctx = new FitnessManagementContext(connectionString);
        }
        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
        public void AddReservation(Reservation reservation) {
            try {
                // Zet een transactie op om fouten te voorkomen
                using var transaction = ctx.Database.BeginTransaction();

                // Schakel IDENTITY_INSERT in voor de hele sessie
                ctx.Database.ExecuteSqlRaw("SET IDENTITY_INSERT reservation ON");


                List<ReservationEF> reservationEFs = MapReservation.MapToDB(reservation, ctx);
                foreach (ReservationEF reservationEF in reservationEFs) {
                    ctx.reservation.Add(reservationEF); // Alleen toevoegen
                }

                SaveAndClear(); // Sla alle wijzigingen op na de loop

                // Schakel IDENTITY_INSERT uit
                ctx.Database.ExecuteSqlRaw("SET IDENTITY_INSERT reservation OFF");

                // Commit de transactie
                transaction.Commit();
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - AddReservation", ex);
            }
        }

        public Reservation GetReservation(int reservationId) {
            try {
                ReservationEF reservationEF = ctx.reservation
                    .Include(x => x.Equipment)
                    .Include(x => x.TimeSlot)
                    .Include(x => x.Member)
                    .AsNoTracking()
                    .FirstOrDefault(r => r.ReservationId == reservationId);


                return MapReservation.MapToDomain(reservationEF, ctx);
            } catch (Exception ex) {
                throw new RepoException("ReservationRepo - GetReservationId", ex);
            }
        }


        public List<Reservation> GetReservationMember(int memberId) {
            try {
                List<ReservationEF> reservationEFs = ctx.reservation.Where(x => x.Member.MemberId == memberId).Include(x => x.Equipment)
                    .Include(x => x.TimeSlot)
                    .Include(x => x.Member).ToList();

                return reservationEFs.Select(x => MapReservation.MapToDomain(x, ctx)).ToList();
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - GetReservationMember", ex);
            }
        }

        public List<Reservation> GetReservationMemberDate(int memberId, DateOnly date) {
            try {

                List<ReservationEF> reservationsEF = ctx.reservation
                    .Where(r => r.Member.MemberId == memberId && r.Date == date)
                    .Include(r => r.TimeSlot) 
                    .Include(r => r.Equipment) 
                    .AsNoTracking()
                    .ToList();

                if (reservationsEF == null) {
                    throw new RepoException("reservation doesn't excist.");
                }
                List<Reservation> reservations = reservationsEF
                    .Select(r => MapReservation.MapToDomain(r, ctx))
                    .ToList();

                List<Reservation> filteredReservation = reservations
                    .GroupBy(r => r.GroupsId).Select(g => g.First()).ToList();

                return filteredReservation;
            } catch (Exception ex) {
                throw new RepoException("ReservationRepo - GetReservationMemberDate", ex);
            }
        }


        public bool IsReservation(int reservationId) {
            try {
                return ctx.reservation.Any(x => x.ReservationId == reservationId);
            } catch (Exception) {

                throw;
            }
        }

        public bool IsTimeSlotAvailable(Reservation reservation) {
            try {
             
                List<int> timeSlots = reservation.TimeSLotEquipment.Keys.ToList();
                List<Equipment> equipments = reservation.TimeSLotEquipment.Values.ToList();

                if (timeSlots.Count == 2) {
                 
                    bool firstSlotAvailable = !ctx.reservation.Any(x =>
                        x.Date == reservation.Date &&
                        x.TimeSlot.TimeSlotId == timeSlots[0] &&
                        x.Equipment.EquipmentId == equipments[0].EquipmentId);

                    
                    bool secondSlotAvailable = !ctx.reservation.Any(x =>
                        x.Date == reservation.Date &&
                        x.TimeSlot.TimeSlotId == timeSlots[1] &&
                        x.Equipment.EquipmentId == equipments[1].EquipmentId);

                  
                    return firstSlotAvailable && secondSlotAvailable;
                } else if (timeSlots.Count == 1) {
                  
                    return !ctx.reservation.Any(x =>
                        x.Date == reservation.Date &&
                        x.TimeSlot.TimeSlotId == timeSlots[0] &&
                        x.Equipment.EquipmentId == equipments[0].EquipmentId);
                }

               
                throw new ArgumentException("Reservation must have at least one time slot.");
            } catch (Exception ex) {
                throw new RepoException("ReservationRepo - IsTimeSlotAvailable", ex);
            }
        }
        public Dictionary<int, List<Equipment>> AvailableTimeSlotDate(DateOnly date) {
            try {
                var allTimeSlots = ctx.time_slot.ToList();

                var allEquipment = ctx.equipment.ToList();

                // Haal alle bezette combinaties van tijdsloten en apparatuur op voor de opgegeven datum
                var occupiedTimeSlotEquipments = ctx.reservation
                    .Where(r => r.Date == date)
                    .Select(r => new { r.TimeSlot.TimeSlotId, r.Equipment.EquipmentId })
                    .ToList();

                var availableTimeSlotEquipments = new Dictionary<int, List<Equipment>>();

                foreach (var timeSlot in allTimeSlots) {
                    // Haal de bezette apparatuur op voor dit specifieke tijdslot
                    var occupiedEquipmentForSlot = occupiedTimeSlotEquipments
                        .Where(o => o.TimeSlotId == timeSlot.TimeSlotId)
                        .Select(o => o.EquipmentId)
                        .ToHashSet();

                    // Filter apparatuur die niet bezet is en niet in onderhoud is
                    var availableEquipment = allEquipment
                        .Where(e => !occupiedEquipmentForSlot.Contains(e.EquipmentId) && !e.IsInMaintenance) // Controleer ook op IsInMaintenance
                        .Select(e => MapEquipment.MapToDomain(e)) // Gebruik de mapper hier
                        .ToList();

                    // Voeg de beschikbare apparatuur toe aan het tijdslot
                    if (availableEquipment.Any()) {
                        availableTimeSlotEquipments[timeSlot.TimeSlotId] = availableEquipment;
                    }
                }

                return availableTimeSlotEquipments;
            } catch (Exception ex) {
                throw new RepoException("ReservationRepo - AvailableTimeSlotDate", ex);
            }
        }







        public void UpdateReservation(Reservation reservation) {
            try {
                List<ReservationEF> reservationEFs = MapReservation.MapToDB(reservation, ctx);
                foreach (ReservationEF reservationEF in reservationEFs) {
                    ctx.reservation.Update(reservationEF);
                }
                SaveAndClear();
            } catch (Exception ex) {
                throw new RepoException("ReservationRepo - UpdateReservation", ex);
            }
        }

        public int GetReservationId() {
            try {
              
                int maxId = ctx.reservation.Any() ? ctx.reservation.Max(r => r.ReservationId) : 0;
                return maxId + 1;
            } catch (Exception ex) {
                throw new RepoException("ReservationRepo - GetReservationId", ex);
            }
        }

        public void DeleteReservation(int groupId) {
            try {
                List<ReservationEF> reservationEFs = ctx.reservation.Where(x => x.GroupsId == groupId).ToList();
                foreach (ReservationEF reservationEF in reservationEFs) {
                    ctx.reservation.Remove(reservationEF);
                }
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - DeleteReservation", ex);
            }
        }
    }
}
