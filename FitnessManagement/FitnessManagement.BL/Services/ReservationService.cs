using FitnessBL.Models;
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
        public Reservation GetReservation(int reservationId) {
            try {
                if (!repo.IsReservation(reservationId)) {
                    throw new ReservationException("GetReservation - Reservatie bestaat niet");
                }

                return repo.GetReservationId(reservationId);
            } catch (Exception ex) {
                throw new ReservationException("GetReservation", ex);
            }
        }


        public List<Reservation> GetReservationsMember(int memberId) {
            try {
                return repo.GetReservationMember(memberId);
            } catch (Exception ex) {
                throw new ReservationException("GetReservationsByMember", ex);
            }
        }

        public bool IsTimeSlotAvailable(Reservation reservation) {
            try {
                return repo.IsTimeSlotAvailable(reservation);
            } catch (Exception ex) {
                throw new ReservationException("IsTimeSlotAvailable", ex);
            }
        }
        public bool IsReservation(int reservationId) {
            try {
                return repo.IsReservation(reservationId);
            } catch (Exception ex) {

                throw new ReservationException("IsReservation", ex);
            }
        }
        public List<Reservation> GetReservationMemberDate(int memberId, DateOnly date) {
            try {
                return repo.GetReservationMemberDate(memberId, date);
            } catch (Exception ex) {

                throw new ReservationException("GetReservationMemberDate", ex);
            }
        }
        public Reservation AddReservation(Reservation reservation) {
            try {
                // Controleer of de reservatie al bestaat
                if (repo.IsReservation(reservation.ReservationId)) {
                    throw new ReservationException("AddReservation - Reservation already exists");
                }

                // Haal alle bestaande reservaties van de gebruiker op voor dezelfde datum
                var existingReservations = GetReservationMemberDate(reservation.Member.MemberId, reservation.Date);

                // Tel het totaal aantal tijdsloten uit alle bestaande reservaties
                int totalTimeSlots = existingReservations.Sum(r => r.TimeSLotEquipment.Count) + reservation.TimeSLotEquipment.Count;

                // Controleer of het totaal aantal tijdsloten de limiet van 4 overschrijdt
                if (totalTimeSlots > 4) {
                    throw new ReservationException("AddReservation - Maximum of 4 time slots per day is reached");
                }
                if (reservation.TimeSLotEquipment.Count == 2) {
                    var TimeSlots = reservation.TimeSLotEquipment.Keys.ToList();
                    if (Math.Abs(TimeSlots[0] - TimeSlots[1]) != 1) {
                        throw new ReservationException("Reservation TimeSlots must be consecutive");
                    }
                }
                // Controleer op conflicten tussen tijdsloten
                foreach (var tijdslot in reservation.TimeSLotEquipment.Keys) {
                    foreach (var existingReservation in existingReservations) {
                        foreach (var existingTijdslot in existingReservation.TimeSLotEquipment.Keys) {
                            // Controleer op exacte overlap
                            if (tijdslot == existingTijdslot) {
                                throw new ReservationException("AddReservation - Overlapping time slots are not allowed.");
                            }

                            // Controleer op directe aansluitingen
                            if (Math.Abs(tijdslot - existingTijdslot) == 1) // Direct aansluitend
                            {
                                // Haal de status op van aansluitende tijdsloten
                                bool hasBefore = existingReservations.Any(r => r.TimeSLotEquipment.Keys.Contains(tijdslot - 1));
                                bool hasAfter = existingReservations.Any(r => r.TimeSLotEquipment.Keys.Contains(tijdslot + 1));

                                if (reservation.TimeSLotEquipment.Count == 1) {
                                    // Als er een slot ervoor en een na is, gooi een uitzondering
                                    if (hasBefore && hasAfter) {
                                        throw new ReservationException("AddReservation - Consecutive time slots limit exceeded.");
                                    }

                                    // Controleer op meer dan 2 aansluitende tijdsloten
                                    if (existingReservations.Count(r => r.TimeSLotEquipment.Keys.Contains(tijdslot - 1)) > 1 ||
                                        existingReservations.Count(r => r.TimeSLotEquipment.Keys.Contains(tijdslot + 1)) > 1) {
                                        throw new ReservationException("AddReservation - Consecutive time slots limit exceeded.");
                                    }
                                } else if (reservation.TimeSLotEquipment.Count == 2) {
                                    // Als de nieuwe reservation al twee slots heeft, mag er niets ervoor of erna zijn
                                    if (hasBefore || hasAfter) {
                                        throw new ReservationException("AddReservation - Consecutive time slots limit exceeded.");
                                    }
                                }
                            }
                        }
                    }
                }



                // Controleer of de nieuwe tijdsloten beschikbaar zijn
                if (!IsTimeSlotAvailable(reservation)) {
                    throw new ReservationException("AddReservation - Timeslot is not available");
                }

                // Voeg de reservatie toe via de repository
                reservation.ReservationId = repo.GetReservationId();
                reservation.GroupsId = reservation.ReservationId;
                repo.AddReservation(reservation);
                return reservation;
            } catch (Exception ex) {
                throw new ReservationException("AddReservation", ex);
            }
        }
        public void DeleteReservation(int groupId) {
            try {
                if (!repo.IsReservation(groupId))
                    throw new ReservationException("UpdateReservation - Reservation Doesn't excist");
                repo.DeleteReservation(groupId);
            } catch (Exception ex) {

                throw new ReservationException("AddReservation", ex);
            }
        }

        public void UpdateReservation(Reservation reservation) {
            try {
                if (!repo.IsReservation(reservation.ReservationId))
                    throw new ReservationException("UpdateReservation - Reservatie bestaat niet");
                var existingReservations = GetReservationMemberDate(reservation.Member.MemberId, reservation.Date);

                // Tel het totaal aantal tijdsloten uit alle bestaande reservaties
                int totalTimeSlots = existingReservations.Sum(r => r.TimeSLotEquipment.Count) + reservation.TimeSLotEquipment.Count;

                // Controleer of het totaal aantal tijdsloten de limiet van 4 overschrijdt
                if (totalTimeSlots > 4) {
                    throw new ReservationException("AddReservation - Maximum of 4 time slots per day is reached");
                }

                // Controleer op conflicten tussen tijdsloten
                foreach (var tijdslot in reservation.TimeSLotEquipment.Keys) {
                    foreach (var existingReservation in existingReservations) {
                        foreach (var existingTijdslot in existingReservation.TimeSLotEquipment.Keys) {
                            // Controleer of de tijdsloten elkaar overlappen
                            if (Math.Abs(tijdslot - existingTijdslot) == 1) {
                                if (reservation.TimeSLotEquipment.Count == 2 || existingReservation.TimeSLotEquipment.Count == 2) {
                                    throw new ReservationException("AddReservation - Overlapping consecutive time slots are not allowed");
                                }
                            }
                        }
                    }
                }
                if (!IsTimeSlotAvailable(reservation)) {
                    throw new ReservationException("AddReservation - Timeslot is not available");
                }
                repo.UpdateReservation(reservation);
            } catch (Exception ex) {
                throw new ReservationException("UpdateReservation", ex);
            }
        }

       

    }
}
