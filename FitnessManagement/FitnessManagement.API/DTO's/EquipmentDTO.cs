namespace FitnessManagement.API.Mapper {
    public class EquipmentDTO {
        public int EquipmentId { get; set; }
        public bool IsInMaintenance { get; set; }
    }
    public class EquipmentString {
        public int EquipmentId { get; set; }
        public string EquipmentType { get; set; }
        public bool IsInMaintenance { get; set; }
    }
    
}
