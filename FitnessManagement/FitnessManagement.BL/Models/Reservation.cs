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
        private DateOnly _date;

        public Reservation() {
        }

        public Reservation(DateOnly date, Equipment equipment, TimeSlot timeSlot, Member member, int groupsId) {
            _date = date;
            Equipment = equipment;
            TimeSlotRes = timeSlot;
            Member = member;
            GroepsId = groupsId;
        }

        public Reservation(DateOnly date, int reservationId, Equipment equipment, TimeSlot timeSlot, Member member, int groupsId) {
            _date = date;
            ReservationId = reservationId;
            Equipment = equipment;
            TimeSlotRes = timeSlot;
            Member = member;
            GroepsId = groupsId;
        }

        public DateOnly Date {
            get { return _date; }
            set {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);

                if (value < today) {
                    throw new ReservationException("Reservation can't happen in the past.");
                }
                if (value > today.AddDays(7)) {
                    throw new ReservationException("Reservation can't be later than a week.");
                }

                _date = value;
            }
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
