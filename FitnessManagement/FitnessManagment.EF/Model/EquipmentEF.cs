﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    
    public class EquipmentEF {
        public EquipmentEF() { }

        public EquipmentEF(int equipmentId, string equipmentType, bool isInMaintenance) {
            EquipmentId = equipmentId;
            Type = equipmentType;
            IsInMaintenance = isInMaintenance;
        }

        [Key]
        [Column("equipment_id")] // Primaire sleutel
        public int EquipmentId { get; set; }

        [Required]
        [Column("device_type", TypeName = "nvarchar(45)")] // Mapping naar kolom device_type
        public string Type { get; set; }

        // Extra kolom, toegevoegd achteraan
        
        [Column(TypeName = "bit")]
        public bool IsInMaintenance { get; set; }
    }
}
