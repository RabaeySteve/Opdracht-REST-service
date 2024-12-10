using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Services {
    public class ReservationService {
        private IReservationRepository repo;

        public ReservationService(IReservationRepository repo) {
            this.repo = repo;
        }
        // Je moet de getmethode zodanig aanpassen dit die kijkt of er al een reservatie voor of na is op die dag voor die member.
        // je gaat een getreservation aanmaken. Daar ga je een meth hebben of het een dubble sessie is of een enkele. En dat op deze manier teruggeven
        public List<Reservation> GetAll() {
            try {
                return repo.GetAll();
            } catch (Exception ex) {

                throw new ReservationException("GetAll");
            }
        }
        public List<Reservation> GetReservationsMemberDate(int memberId, DateTime date) {
            try {
                return repo.GetReservationsMemberDate(memberId, date);
            } catch (Exception ex) {

                throw new ReservationException("GetReservationsMemberDate");
            }
        }
        public List<Reservation> GetReservationsMember(int memberId) {
            try {
                return repo.GetReservationsMember(memberId);
            } catch (Exception ex) {

                throw new ReservationException("GetReservations");
            }
        }
        public Reservation GetReservation(int id) {
            try {
                return repo.GetReservation(id);
            } catch (Exception ex) {

                throw new ReservationException("GetReservation");
            }
        }
        public bool IsReservation(int reservationId) {
            try {
                return repo.IsReservation(reservationId);
            } catch (Exception ex) {

                throw new ReservationException("IsReservation");
            }
        }
        public Reservation AddReservation(Reservation reservation) {
            try {
                if (!IsReservation(reservation.ReservationId)) {
                    throw new ReservationException("Reservation bestaat niet");
                } else {
                    int nextId = 0;
                    List<Reservation> all = new List<Reservation>();
                    all = repo.GetAll();
                    nextId = all.Count + 1;
                    reservation.ReservationId = nextId;
                    bool Add = true;
                    List<Reservation> reservationsDateMember = new List<Reservation>();
                    reservationsDateMember = GetReservationsMemberDate(reservation.Member.MemberId, reservation.Date);
                    if (reservationsDateMember.Count >= 4) {
                        throw new ReservationException("You can only make 4 reservations a day");
                    }
                    else if(reservationsDateMember.Count > 1)
                    {
                        
                        foreach (Reservation r in reservationsDateMember) {
                            if (r.Equipment.EquipmentId == reservation.Equipment.EquipmentId) {
                                if (reservation.TimeSlot.StartTime == r.TimeSlot.StartTime + 1 || reservation.TimeSlot.StartTime == r.TimeSlot.StartTime - 1) {
                                    if (reservation.TimeSlot.StartTime == r.TimeSlot.StartTime + 2 || reservation.TimeSlot.StartTime == r.TimeSlot.StartTime - 2) {
                                        Add = false;
                                        throw new ReservationException("Can't make 3 reservations in a row");

                                    } 
                                }
                            }
                           
                        }
                    }
                    if (Add) {
                        repo.AddReservation(reservation);
                    }
                    
                    return reservation;
                }
            } catch (Exception ex) {

                throw new ReservationException("AddReservation");
            }

        }
        public void AddDubbleRes(Reservation reservation) {
            try {
                // Je gaat hier bij het testen van de service in de api 2 keer add moeten aanroepen en repo manier toepassen dan de repo verijderen
            } catch (Exception) {

                throw;
            }
        }
        public void DeleteReservation(Reservation reservation) {
            try {
                if (!IsReservation(reservation.ReservationId)) {
                    throw new ReservationException("Reservation bestaat niet");
                } else {
                    repo.DeleteReservation(reservation);
                    
                }
            } catch (Exception ex) {

                throw new ReservationException("DeleteReservation");
            }

        }
        public Reservation UpdateReservation(Reservation reservation) {
            try {
                if (!IsReservation(reservation.ReservationId)) {
                    throw new ReservationException("Reservation bestaat niet");
                } else {

                    repo.UpdateReservation(reservation);
                    return reservation;
                }
            } catch (Exception ex) {

                throw new ReservationException("AddReservation");
            }

        }
    }
}
