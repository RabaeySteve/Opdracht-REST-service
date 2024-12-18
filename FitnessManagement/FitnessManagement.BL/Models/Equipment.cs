using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Models {
    public class Equipment {
       
        public Equipment() {
        }

        public Equipment(int equipmentId, EquipmentType type, bool isInMaintenance) {
            EquipmentId = equipmentId;
            Type = type;
            IsInMaintenance = isInMaintenance;
        }

        public Equipment(EquipmentType type) {
            Type = type;
            IsInMaintenance = false;
        }

        public int EquipmentId { get; set; }
        public EquipmentType Type { get; set; }
        public bool IsInMaintenance { get; set; }

        public enum EquipmentType {
            treadmill,
            bike
        }
        public override string? ToString() {
            return $"Equipment: {Type} InMaintenance? {IsInMaintenance}";
        }
    }
}
