using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    public class EquipmentEF {
        public EquipmentEF() { }

        public EquipmentEF(int equipmentId, string deviceType) {
            EquipmentId = equipmentId;
            DeviceType = deviceType;
        }

        [Key]
        public int EquipmentId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(45)")]
        public string DeviceType { get; set; }
    }
}

