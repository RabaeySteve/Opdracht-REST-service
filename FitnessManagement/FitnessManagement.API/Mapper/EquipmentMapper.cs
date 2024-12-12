using FitnessManagement.BL.Models;

namespace FitnessManagement.API.Mapper
{
    public class EquipmentMapper
    {
        public static Equipment MapEquipment(EquipmentDTO equipmentDTO) {
            return new Equipment {
                EquipmentId = equipmentDTO.EquipmentId,
                IsInMaintenance = equipmentDTO.IsInMaintenance
            };
        }
    }
}
