using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    
    public class ReservationEF {
        public ReservationEF() { }

        public ReservationEF(int reservationId, DateOnly date, EquipmentEF equipment, TimeSlotEF timeSlot, MemberEF member, int groupsId) {
            ReservationId = reservationId;
            Date = date;
            Equipment = equipment;
            TimeSlot = timeSlot;
            Member = member;
            GroupsId = groupsId;
        }

        [Key]
        [Column("reservation_id")]
        public int ReservationId { get; set; }

        //[Required]
        //[Column("equipment_id")]
        //public int EquipmentId { get; set; }

        [ForeignKey("equipment_id")]
        public EquipmentEF Equipment { get; set; }

        //[Required]
        //[Column("time_slot_id")]
        //public int TimeSlotId { get; set; }

        [ForeignKey("time_slot_id")]
        public TimeSlotEF TimeSlot { get; set; }

        [Required]
        [Column("date", TypeName = "date")]
        public DateOnly Date { get; set; }

        //[Required]
        //[Column("member_id")]
        //public int MemberId { get; set; }

        [ForeignKey("member_id")]
        public MemberEF Member { get; set; }

        [Column("groups_id")]
        public int GroupsId { get; set; }
    }
}
