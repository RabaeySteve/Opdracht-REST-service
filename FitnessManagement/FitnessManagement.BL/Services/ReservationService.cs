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
        public bool IsAvailable(DateTime reservationDate, int equipmentId, int timeslot) {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(7);

           
            var reservations = repo.GetAll();


            var equipment = repo.GetEquipment(equipmentId);

            
            
            bool isAvailable = true;
            if (reservationDate.Date >= startDate && reservationDate.Date < endDate) {
                if (!equipment.IsInMaintenance) {
                    foreach (var reservation in reservations) {

                        if (reservation.Equipment.EquipmentId == equipmentId &&
                            reservation.Date == reservationDate && reservation.TimeSlotRes.TimeSlotId == timeslot) {
                            isAvailable = false;
                            break;
                        }
                    }

                } else {
                    isAvailable = false;
                }
            } else {
                isAvailable = false;
            }
            
               
                return isAvailable;
            

            
        }
        public List<Equipment> GetAllEquipment() {
            try {
                return repo.GetAllEquipment();
            } catch (Exception ex) {

                throw;
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
        public List<Reservation> GetReservation(int id) {
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
        public Reservation AddReservation(Reservation reservation, bool dubbleReservation) {
            try {
                // Controleer of het tijdslot en de apparatuur beschikbaar zijn
                if (!IsAvailable(reservation.Date, reservation.Equipment.EquipmentId, reservation.TimeSlotRes.TimeSlotId)) {
                    throw new ReservationException("The equipment is not available for the selected date.");
                }
                reservation.ReservationId = repo.GetAll().Count + 1;
                reservation.GroepsId = repo.GetAll().Count + 1;
                // Controleer of de reservatie-ID al bestaat
                if (IsReservation(reservation.ReservationId)) {
                    throw new ReservationException("Reservation already exists.");
                }

                // Controleer of de member al 4 reserveringen heeft op dezelfde dag
                var reservationsDateMember = GetReservationsMemberDate(reservation.Member.MemberId, reservation.Date);
                if (reservationsDateMember.Count >= 4) {
                    throw new ReservationException("You can only make 4 reservations per day.");
                }

                // Controleer of er niet meer dan 2 opeenvolgende tijdslots zijn gereserveerd
                foreach (var r in reservationsDateMember) {
                    if (r.Equipment.EquipmentId == reservation.Equipment.EquipmentId) {
                        int timeslotR = r.TimeSlotRes.TimeSlotId;
                        int timeslot = reservation.TimeSlotRes.TimeSlotId;
                        if (timeslotR == (timeslot + 2) || timeslotR == (timeslot - 2)) {
                            throw new ReservationException("Cannot reserve more than 2 consecutive time slots.");
                        }

                    }
                }
                
                // Voeg de reservering toe
                var secondReservation = new Reservation();
                if (dubbleReservation) {

                  
                    // Maak de tweede reservering aan met het volgende tijdslot
                    var secondTimeSlot = new TimeSlot {
                        TimeSlotId = reservation.TimeSlotRes.TimeSlotId + 1 // Volgend tijdslot
                    };

                    secondReservation = new Reservation{
                        Member = reservation.Member,
                        Equipment = reservation .Equipment,
                        Date = reservation.Date,
                        TimeSlotRes = secondTimeSlot
                    };
                    if (!IsAvailable(secondReservation.Date, secondReservation.Equipment.EquipmentId, reservation.TimeSlotRes.TimeSlotId)) {
                        throw new ReservationException("The equipment is not available for the second time slot.");
                    }
                    
                    secondReservation.GroepsId =reservation.GroepsId;

                    var reservationsDateMember2 = GetReservationsMemberDate(secondReservation.Member.MemberId, secondReservation.Date);
                    if (reservationsDateMember2.Count >= 3) {
                        throw new ReservationException("You can only make 4 reservations per day.");
                    }

                    // Controleer of er niet meer dan 2 opeenvolgende tijdslots zijn gereserveerd
                    foreach (var r in reservationsDateMember2) {
                        if (r.Equipment.EquipmentId == secondReservation.Equipment.EquipmentId) {
                            int timeslotR = r.TimeSlotRes.TimeSlotId;
                            int timeslotSecond = secondReservation.TimeSlotRes.TimeSlotId;
                            if (timeslotR == (timeslotSecond +1) || timeslotR == (timeslotSecond - 2)) {
                                throw new ReservationException("Cannot reserve more than 2 consecutive time slots.");
                            }

                        }
                    }
                }
                repo.AddReservation(reservation);
                if (dubbleReservation) {
                    secondReservation.ReservationId = repo.GetAll().Count + 1;
                    repo.AddReservation(secondReservation);
                }
               
                

                return reservation;
            } catch (Exception ex) {
                throw new ReservationException($"AddReservation failed: {ex.Message}");
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
        public Reservation UpdateReservation(Reservation reservation, bool dubbleReservation) {
            try {
                if (!IsReservation(reservation.ReservationId)) {
                    throw new ReservationException("Reservation bestaat niet");
                } else {
                    DeleteReservation(reservation);
                    AddReservation(reservation, dubbleReservation);
                    return reservation;
                }
            } catch (Exception ex) {

                throw new ReservationException("AddReservation");
            }

        }
    }
}
