using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessManagement.BL.Services {
    public class ReservationService {
        private readonly IReservationRepository repo;

        public ReservationService(IReservationRepository repo) {
            this.repo = repo;
        }

        public Reservation GetReservation(int reservationId) {
            try {
                if (!repo.IsReservation(reservationId)) {
                    throw new ReservationException("GetReservation - Reservation does not exist");
                }

                return repo.GetReservation(reservationId);
            } catch (ReservationException) {
                throw;
            } catch (Exception ex) {
                throw new ReservationException("GetReservation - Unexpected error occurred", ex);
            }
        }

        public List<Reservation> GetReservationsMember(int memberId) {
            try {
                return repo.GetReservationMember(memberId);
            } catch (ReservationException) {
                throw;
            } catch (Exception ex) {
                throw new ReservationException("GetReservationsByMember - Unexpected error occurred", ex);
            }
        }

        public bool IsTimeSlotAvailable(Reservation reservation) {
            try {
                return repo.IsTimeSlotAvailable(reservation);
            } catch (ReservationException) {
                throw;
            } catch (Exception ex) {
                throw new ReservationException("IsTimeSlotAvailable - Unexpected error occurred", ex);
            }
        }

        public bool IsReservation(int reservationId) {
            try {
                return repo.IsReservation(reservationId);
            } catch (ReservationException) {
                throw;
            } catch (Exception ex) {
                throw new ReservationException("IsReservation - Unexpected error occurred", ex);
            }
        }

        public List<Reservation> GetReservationMemberDate(int memberId, DateOnly date) {
            try {
                return repo.GetReservationMemberDate(memberId, date);
            } catch (ReservationException) {
                throw;
            } catch (Exception ex) {
                throw new ReservationException("GetReservationMemberDate - Unexpected error occurred", ex);
            }
        }

        public Reservation AddReservation(Reservation reservation) {
            try {
                if (repo.IsReservation(reservation.ReservationId)) {
                    throw new ReservationException("AddReservation - Reservation already exists");
                }

                var existingReservations = GetReservationMemberDate(reservation.Member.MemberId, reservation.Date) ?? new List<Reservation>();

                int totalTimeSlots = existingReservations.Sum(r => r.TimeSLotEquipment?.Count ?? 0)
                                    + (reservation.TimeSLotEquipment?.Count ?? 0);

                if (totalTimeSlots > 4) {
                    throw new ReservationException("AddReservation - Maximum of 4 time slots per day is reached");
                }

                if (reservation.TimeSLotEquipment?.Count == 2) {
                    var timeSlots = reservation.TimeSLotEquipment.Keys.ToList();
                    if (Math.Abs(timeSlots[0] - timeSlots[1]) != 1) {
                        throw new ReservationException("AddReservation - TimeSlots must be consecutive");
                    }
                }

                foreach (var tijdslot in reservation.TimeSLotEquipment?.Keys ?? Enumerable.Empty<int>()) {
                    foreach (var existingReservation in existingReservations) {
                        foreach (var existingTijdslot in existingReservation.TimeSLotEquipment?.Keys ?? Enumerable.Empty<int>()) {
                            if (tijdslot == existingTijdslot) {
                                throw new ReservationException("AddReservation - Overlapping time slots are not allowed.");
                            }
                        }
                    }
                }

                if (!IsTimeSlotAvailable(reservation)) {
                    throw new ReservationException("AddReservation - Timeslot is not available");
                }

                reservation.ReservationId = repo.GetReservationId();
                reservation.GroupsId = reservation.ReservationId;
                repo.AddReservation(reservation);
                return reservation;
            } catch (ReservationException) {
                throw;
            } catch (Exception ex) {
                throw new ReservationException("AddReservation - Unexpected error occurred", ex);
            }
        }

        public void DeleteReservation(int groupId) {
            try {
                if (!repo.IsReservation(groupId)) {
                    throw new ReservationException("DeleteReservation - Reservation does not exist");
                }
                repo.DeleteReservation(groupId);
            } catch (ReservationException) {
                throw;
            } catch (Exception ex) {
                throw new ReservationException("DeleteReservation - Unexpected error occurred", ex);
            }
        }

        public void UpdateReservation(Reservation reservation) {
            try {
                if (!repo.IsReservation(reservation.ReservationId)) {
                    throw new ReservationException("UpdateReservation - Reservation does not exist");
                }

                var existingReservations = GetReservationMemberDate(reservation.Member.MemberId, reservation.Date) ?? new List<Reservation>();

                int totalTimeSlots = (existingReservations?.Sum(r => r.TimeSLotEquipment?.Count ?? 0) ?? 0)
                                    + (reservation.TimeSLotEquipment?.Count ?? 0);

                if (totalTimeSlots > 4) {
                    throw new ReservationException("UpdateReservation - Maximum of 4 time slots per day is reached");
                }

                foreach (var tijdslot in reservation.TimeSLotEquipment?.Keys ?? Enumerable.Empty<int>()) {
                    foreach (var existingReservation in existingReservations) {
                        foreach (var existingTijdslot in existingReservation.TimeSLotEquipment?.Keys ?? Enumerable.Empty<int>()) {
                            if (tijdslot == existingTijdslot) {
                                throw new ReservationException("UpdateReservation - Overlapping time slots are not allowed.");
                            }
                        }
                    }
                }

                if (!IsTimeSlotAvailable(reservation)) {
                    throw new ReservationException("UpdateReservation - Timeslot is not available");
                }

                repo.UpdateReservation(reservation);
            } catch (ReservationException) {
                throw;
            } catch (Exception ex) {
                throw new ReservationException("UpdateReservation - Unexpected error occurred", ex);
            }
        }

        public Dictionary<int, List<Equipment>> AvailableTimeSlotDate(DateOnly date) {
            try {
                return repo.AvailableTimeSlotDate(date);
            } catch (ReservationException) {
                throw;
            } catch (Exception ex) {
                throw new ReservationException("AvailableTimeSlotDate - Unexpected error occurred", ex);
            }
        }
    }
}
