using FitnessBL.Models;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface IReservationRepository {
        int GetReservationId();
        Reservation GetReservationId(int reservationId);
        
        List<Reservation> GetReservationMember(int memberId);
        List<Reservation> GetReservationMemberDate(int memberId, DateOnly date);
        bool IsTimeSlotAvailable(Reservation reservation);
        bool IsReservation(int reservationId);
        void AddReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void DeleteReservation(int groupId);
     
    }
}
