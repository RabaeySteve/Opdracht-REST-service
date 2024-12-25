using FitnessManagement.API.Exceptions;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.Mapper
{
    public class EquipmentMapper
    {
        public static Equipment MapEquipment(EquipmentDTO equipmentDTO) {
            try {
                return new Equipment {
                    EquipmentId = equipmentDTO.EquipmentId,
                    IsInMaintenance = equipmentDTO.IsInMaintenance
                };
            } catch (Exception ex) {

                throw new MapperException("MapEquipment", ex);
            }
          
        }
    }
}
