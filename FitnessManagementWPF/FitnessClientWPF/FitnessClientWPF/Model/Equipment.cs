using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FitnessClientWPF.Model {
    public class Equipment {
        public int EquipmentId { get; set; }
        
        public string EquipmentType { get; set; }

        public bool IsInMaintenance { get; set; }
    }

    
}
