using FitnessManagement.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Models
{
    public class Reservation
    {
        private DateTime _date;

        public Reservation() {
        }

        public Reservation(DateTime date, int reservationId, int equipmentId, int timeSlotId, int memberId) {
            Date = date;
            ReservationId = reservationId;
            EquipmentId = equipmentId;
            TimeSlotId = timeSlotId;
            MemberId = memberId;
        }

        public DateTime Date {
            get { return _date; }
            set {
                if (value.Date < DateTime.Now.Date ) {
                    throw new ReservationException("Reservation can't happen in the past.");
                } if (value.Date > DateTime.Now.AddDays(7)) {
                    throw new ReservationException("Reservation can't be later then a week.");
                }
                
                _date = value; }
        }



        public int ReservationId { get; set; }
        public int EquipmentId { get; set; }
        public int TimeSlotId { get; set; }
        public int MemberId { get; set; }



        public override string ToString() {
            return $"Reservation ID: {ReservationId}, Member: {MemberId}, Equipment: {EquipmentId}, Date: {Date.ToString()}, TimeSlot: {TimeSlotId}";
        }
    }
}
