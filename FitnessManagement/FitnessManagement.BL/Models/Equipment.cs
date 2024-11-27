using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Models {
    public class Equipment {
        public enum EquipmentType {
            treadmill,
            bike
        }
        public Equipment() {
        }

        public Equipment(int equipmentId, EquipmentType type) {
            EquipmentId = equipmentId;
            Type = type;
        }

        
        public int EquipmentId { get; set; }
        public EquipmentType Type { get; set; }

        public override string? ToString() {
            return $"Equipment: {Type}";
        }
    }
}
