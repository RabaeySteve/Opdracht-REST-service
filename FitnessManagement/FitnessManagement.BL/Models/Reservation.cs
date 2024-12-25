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
            TimeSLotEquipment = new Dictionary<int, Equipment>();
        }

        public Reservation(DateOnly date, int groepsId, int reservationId, Member member) {
            _date = date;
            GroupsId = groepsId;
            ReservationId = reservationId;
            TimeSLotEquipment = new Dictionary<int, Equipment>();
            Member = member;
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



        public int GroupsId { get; set; }
        public int ReservationId { get; set; }
        public Dictionary<int, Equipment> TimeSLotEquipment { get; set; }
        public Member Member { get; set; }



        
    }
}
