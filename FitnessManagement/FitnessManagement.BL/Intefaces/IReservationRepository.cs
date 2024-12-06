using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface IReservationRepository {
        IEnumerable<Reservation> GetAll();
        IEnumerable<Reservation> GetReservations(int memberId);

        Reservation GetReservation(int reservationId);
        bool IsReservation(int reservationId);
        Reservation GetReservationId(Reservation reservation);
        void AddReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
    }
}
