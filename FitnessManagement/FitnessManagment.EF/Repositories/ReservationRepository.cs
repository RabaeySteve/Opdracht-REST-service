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

        public ReservationRepository(string connectioString) {
            this.ctx = new FitnessManagementContext(connectioString);
        }
        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
        public List<Reservation> GetReservationsMemberDate(int memberId, DateTime date) {
            try {
                List<Reservation> reservationsMember = new List<Reservation>();
                reservationsMember = GetReservationsMember(memberId);
                List<Reservation>  reservationsMemberDate = new List<Reservation>();
                foreach (Reservation reservation in reservationsMember) {
                    if (reservation.Date == date) {
                        reservationsMemberDate.Add(reservation);
                    }
                }
                return reservationsMemberDate;
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - GetReservationsMemberDate", ex);
            }
        }
       
        public List<Equipment> GetAllEquipment() {
            try {
                return ctx.equipment.Select(x => MapEquipment.MapToDomain(x)).ToList();
            } catch (Exception ex) {

                throw;
            }
        }
        public Equipment GetEquipment(int id) {
            try {

                return MapEquipment.MapToDomain(ctx.equipment.Where(x => x.EquipmentId == id).AsNoTracking().FirstOrDefault());
            } catch (Exception ex) {
                throw new RepoException("EquipmentRepo - GetEquipment", ex);
            }
        }




        public void DeleteReservation(Reservation reservation) {
            try {
                List<ReservationEF> reservationEFs = ctx.reservation.Where(x => x.GroupsId == reservation.GroepsId).ToList();
                foreach (ReservationEF reservationEF in reservationEFs) {
                    if (reservationEF != null) {
                        ctx.reservation.Remove(reservationEF);
                        
                    }
                }
                SaveAndClear();


            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - DeleteReservation", ex);
            }
        }

        public List<Reservation> GetAll() {
            try {
                List<ReservationEF> reservationEFs =  ctx.reservation.Include(x => x.Member).Include(x => x.Equipment).Include(x => x.TimeSlot).ToList();
                    
                return  reservationEFs.Select(x => MapReservation.MapToDomain(x, ctx)).ToList();
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - GetAll", ex);
            }
        }

        public List<Reservation> GetReservation(int groupsId) {
            try {
                List<ReservationEF> reservationEF = ctx.reservation
                    .Where(x => x.GroupsId == groupsId) // Filter de reserveringen op GroupId
                    .Include(x => x.Member)             // Voeg gerelateerde navigatie-eigenschappen toe
                    .Include(x => x.Equipment)
                    .Include(x => x.TimeSlot)
                    .ToList();

                List<Reservation> reservations = reservationEF.Select(x => MapReservation.MapToDomain(x, ctx)).ToList(); // Pas de mapping toe
                return reservations;                  // Materialiseer de query als lijst
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - GetReservationsMember", ex);
            }
        }

        public List<Reservation> GetReservationsMember(int memberId) {
            try {
                List<ReservationEF> reservationEFs = ctx.reservation.Where(x => x.Member.MemberId == memberId).ToList();
                return reservationEFs.Select(x => MapReservation.MapToDomain(x, ctx)).ToList();

            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - GetReservation", ex);
            }
        }

        public bool IsReservation(int reservationId) {
            try {

                bool excist = ctx.reservation.Any(x => x.ReservationId == reservationId);
                return excist;
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - IsReservation", ex);
            }
        }
        public void AddReservation(Reservation reservation) {
            try {
                ctx.reservation.Add(MapReservation.MapToDB(reservation, ctx));
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - AddReservation", ex);
            }
        }
        //public void AddDubbleRes(Reservation reservation) {
        //    try {
        //        AddReservation(reservation);
        //        int reservationId = reservation.ReservationId;
        //        int timeslot = reservation.TimeSlot.TimeSlotId;
        //        Reservation newReservation = reservation;
        //        newReservation.ReservationId = reservationId + 1;
        //        newReservation.TimeSlot.TimeSlotId = timeslot + 1;
        //        AddReservation(newReservation);
        //        SaveAndClear();
        //    } catch (Exception ex) {

        //        throw new RepoException("ReservationRepo - AddDubbleRes", ex);
        //    }
        //}
        //public void UpdateReservation(Reservation reservation) {
        //    try {
        //        DeleteReservation(reservation);

        //        AddReservation(reservation);
        //        SaveAndClear();
        //    } catch (Exception ex) {

        //        throw new RepoException("ReservationRepo - UpdateReservation", ex);
        //    }
        //}

      

        public void UpdateDubbleRes(Reservation reservation) {
            try {
                DeleteReservation(reservation);
                AddReservation(reservation);
                int reservationId = reservation.ReservationId;
                int timeslot = reservation.TimeSlotRes.TimeSlotId;
                Reservation newReservation = reservation;
                newReservation.ReservationId = reservationId +1;
                newReservation.TimeSlotRes.TimeSlotId = timeslot + 1;
                DeleteReservation(newReservation);
                
                AddReservation(newReservation);
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - UpdateDubbleRes", ex);
            }
        }

        
    }
}
