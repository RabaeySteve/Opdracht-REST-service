using FitnessBL.Models;
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

        public Reservation(DateTime date, Equipment equipment, TimeSlot timeSlot, Member member, int groupsId) {
            _date = date;
            Equipment = equipment;
            TimeSlotRes = timeSlot;
            Member = member;
            GroepsId = groupsId;
        }

        public Reservation(DateTime date, int reservationId, Equipment equipment, TimeSlot timeSlot, Member member, int groupsId) {
            _date = date;
            ReservationId = reservationId;
            Equipment = equipment;
            TimeSlotRes = timeSlot;
            Member = member;
            GroepsId = groupsId;
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


        public int GroepsId { get; set; }
        public int ReservationId { get; set; }
        public Equipment  Equipment { get; set; }
        public TimeSlot TimeSlotRes { get; set; }
        public Member Member { get; set; }



        public override string ToString() {
            return $"Reservation ID: {ReservationId}, Member: {Member.FirstName}, Equipment: {Equipment.Type}, Date: {Date.ToString()}, TimeSlot: {TimeSlotRes.StartTime}";
        }
    }
}
