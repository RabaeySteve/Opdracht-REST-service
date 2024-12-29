using FitnessManagement.API.Exceptions;
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
        public static EquipmentString MapEquipmentTypeToStirng(Equipment equipment) {
           
                return new EquipmentString {
                    EquipmentId = equipment.EquipmentId,
                    EquipmentType = equipment.Type.ToString(),
                    IsInMaintenance = equipment.IsInMaintenance
                };
           

        }
    }
}
