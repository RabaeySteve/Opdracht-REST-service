using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FitnessManagement.BL.Models.Equipment;

namespace FitnessManagement.EF.Model {
    public class EquipmentEF {
        public EquipmentEF() { }

        public EquipmentEF(int equipmentId, string equipmentType, bool isInMainenance) {
            EquipmentId = equipmentId;
            Type = equipmentType;
            IsInMaintenance = isInMainenance;
        }

        [Key]
        public int EquipmentId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(45)")]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "bit")] 
        public bool IsInMaintenance { get; set; } 
    }
}

