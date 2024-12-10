using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface IReservationRepository {
        List<Reservation> GetAll();
        List<Reservation> GetReservationsMember(int memberId);
        List<Reservation> GetReservationsMemberDate(int memberId, DateTime date);
        Reservation GetReservation(int reservationId);
        bool IsReservation(int reservationId);
       
        void AddReservation(Reservation reservation);
        void AddDubbleRes(Reservation reservation);
       
        void DeleteReservation(Reservation reservation);
        void DeleteDubbleRes(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void UpdateDubbleRes(Reservation reservation);
    }
}
