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

        public ReservationEF(int reservationId, DateTime date, EquipmentEF equipment, TimeSlotEF timeSlot, MemberEF member, int groupsId) {
            ReservationId = reservationId;
            Date = date;
            Equipment = equipment;
            TimeSlot = timeSlot;
            Member = member;
            GroupsId = groupsId;
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReservationId { get; set; }

        [Required]
        public int GroupsId {  get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Foreign key naar Equipment
        
        public EquipmentEF Equipment { get; set; }

        // Foreign key naar TimeSlot
       
        public TimeSlotEF TimeSlot { get; set; }

        // Foreign key naar Member
        
        public MemberEF Member { get; set; }
    }
}
