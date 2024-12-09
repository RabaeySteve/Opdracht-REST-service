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


        public void AddReservation(Reservation reservation) {
            try {
                ctx.reservation.Add(MapReservation.MapToDB(reservation, ctx));
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - AddReservation", ex);
            }
        }

        public void DeleteReservation(Reservation reservation) {
            try {
                ReservationEF reservationEF = ctx.reservation.Where(x => x.ReservationId == reservation.ReservationId).FirstOrDefault();
                if (reservationEF != null) {
                    ctx.reservation.Remove(reservationEF);
                    SaveAndClear();
                }
                
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - DeleteReservation", ex);
            }
        }

        public List<Reservation> GetAll() {
            try {
                return ctx.reservation.Include(x => x.Member).Include(x => x.Equipment).Include(x => x.TimeSlot).Select(x => MapReservation.MapToDomain(x)).ToList();
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - GetAll", ex);
            }
        }

        public Reservation GetReservation(int reservationId) {
            try {
                return MapReservation.MapToDomain(ctx.reservation.Where(x => x.ReservationId == reservationId).Include(x => x.Member).Include(x => x.Equipment).Include(x => x.TimeSlot).FirstOrDefault());
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - GetReservationsMember", ex);
            }
        }

        public List<Reservation> GetReservationsMember(int memberId) {
            try {
                return ctx.reservation.Where(x => x.Member.MemberId == memberId)
                    .Select(x=> MapReservation.MapToDomain(x)).ToList();
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - GetReservation", ex);
            }
        }

        public bool IsReservation(int reservationId) {
            try {
                return ctx.reservation.Any(x => x.ReservationId == reservationId);
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - IsReservation", ex);
            }
        }

        public void UpdateReservation(Reservation reservation) {
            try {
                DeleteReservation(reservation);
                AddReservation(reservation);
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepoException("ReservationRepo - UpdateReservation", ex);
            }
        }

       

        
    }
}
