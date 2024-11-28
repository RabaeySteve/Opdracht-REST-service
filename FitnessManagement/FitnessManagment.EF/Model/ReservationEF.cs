using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitnessManagement.BL.Models;

namespace FitnessManagement.EF.Model {
    public class ReservationEF {
        public ReservationEF() { }

        public ReservationEF(int reservationId, DateTime date, int equipmentId, int timeSlotId, int memberId) {
            ReservationId = reservationId;
            Date = date;
            EquipmentId = equipmentId;
            TimeSlotId = timeSlotId;
            MemberId = memberId;
        }

        [Key]
        public int ReservationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Foreign key naar Equipment
        public int EquipmentId { get; set; }
        public EquipmentEF Equipment { get; set; }

        // Foreign key naar TimeSlot
        public int TimeSlotId { get; set; }
        public TimeSlotEF TimeSlot { get; set; }

        // Foreign key naar Member
        public int MemberId { get; set; }
        public MemberEF Member { get; set; }
    }
}
