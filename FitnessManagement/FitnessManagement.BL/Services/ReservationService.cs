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
        public bool IsAvailable(DateOnly reservationDate, int equipmentId, int timeslot) {
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly endDate = startDate.AddDays(7);

            // Controleer of de reservatiedatum binnen de geldige periode valt
            if (reservationDate < startDate || reservationDate >= endDate) {
                return false;
            }

            var equipment = repo.GetEquipment(equipmentId);
            if (equipment.IsInMaintenance) {
                return false;
            }

            var reservations = repo.GetAll();

            // Controleer of de opgegeven tijdslot al is gereserveerd
            foreach (var reservation in reservations) {
                if (reservation.Equipment.EquipmentId == equipmentId &&
                    reservation.Date == reservationDate &&
                    reservation.TimeSlotRes.TimeSlotId == timeslot) {
                    return false;
                }
            }

            return true; // Beschikbaar
        }

        public List<Equipment> GetAllEquipment() {
            try {
                return repo.GetAllEquipment();
            } catch (Exception ex) {

                throw;
            }
        }

        public List<Reservation> GetReservationsMemberDate(int memberId, DateOnly date) {
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
        private Dictionary<DateOnly, Dictionary<int, int>> _reservationsByDate = new Dictionary<DateOnly, Dictionary<int, int>>();


        public List<Reservation> AddReservation(List<Reservation> reservations) {
            var addedReservations = new List<Reservation>();

            foreach (var reservation in reservations) {
                // 1. Controleer beschikbaarheid van het tijdslot en apparatuur
                if (!IsAvailable(reservation.Date, reservation.Equipment.EquipmentId, reservation.TimeSlotRes.TimeSlotId)) {
                    throw new ReservationException($"TimeSlot {reservation.TimeSlotRes.TimeSlotId} is not available for Equipment {reservation.Equipment.Type}.");
                }

                // 2. Genereer nieuw ReservationId en GroepsId
                if (reservation.ReservationId == 0) {
                    reservation.ReservationId = repo.GetAll().Count + 1;
                    reservation.GroepsId = reservation.ReservationId; // Initieel groepsId = ReservationId
                }

                // 3. Controleer of de reservering al bestaat
                if (IsReservation(reservation.ReservationId)) {
                    throw new ReservationException("Reservation already exists.");
                }

                // 4. Controleer maximaal 4 reserveringen per dag voor de member
                var reservationsForDate = GetReservationsMemberDate(reservation.Member.MemberId, reservation.Date);
                if (reservationsForDate.Count >= 4) {
                    throw new ReservationException("You can only make 4 reservations per day.");
                }

                // 5. Controleer opeenvolgende tijdslots
                foreach (var existing in reservationsForDate) {
                    if (existing.Equipment.EquipmentId == reservation.Equipment.EquipmentId) {
                        int existingTimeSlot = existing.TimeSlotRes.TimeSlotId;
                        if (Math.Abs(existingTimeSlot - reservation.TimeSlotRes.TimeSlotId) > 1) {
                            throw new ReservationException("Cannot reserve more than 2 consecutive time slots.");
                        }
                    }
                }

                // 6. Voeg de reservering toe
                repo.AddReservation(reservation);
                addedReservations.Add(reservation);

                // 7. Maak een tweede reservering indien nodig
                if (reservations.Count == 2) {
                    var secondReservation = reservations[1];
                    secondReservation.ReservationId = reservation.ReservationId + 1; // Uniek ID
                    secondReservation.GroepsId = reservation.GroepsId; // Zelfde GroepsId

                    // Controleer beschikbaarheid van de tweede reservering
                    if (!IsAvailable(secondReservation.Date, secondReservation.Equipment.EquipmentId, secondReservation.TimeSlotRes.TimeSlotId)) {
                        throw new ReservationException($"TimeSlot {secondReservation.TimeSlotRes.TimeSlotId} is not available for Equipment {secondReservation.Equipment.Type}.");
                    }

                    repo.AddReservation(secondReservation);
                    addedReservations.Add(secondReservation);
                }
            }

            return addedReservations;
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
                    reservation.GroepsId = reservation.ReservationId;
                    DeleteReservation(reservation);
                    //AddReservation(reservation, dubbleReservation);
                    return reservation;
                }
            } catch (Exception ex) {

                throw new ReservationException("UpdateReservation");
            }

        }
    }
}
