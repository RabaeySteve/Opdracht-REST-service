using FitnessBL.Models;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitnessBL.Models.Member;
using static FitnessManagement.BL.Models.Equipment;

namespace FitnessManagement.EF.Mappers {
    public class MapEquipment {
        public static Equipment MapToDomain(EquipmentEF db) {
            try {
                return new Equipment(
                      db.EquipmentId,
                      MapStringToEquipmentType(db.Type),
                      db.IsInMaintenance
                    );
            } catch (Exception ex) {

                throw new MapException("MapEquipment - MapToDomain", ex);
            }
        }
        public static EquipmentEF MapToDB(Equipment e) {
            try {
                return new EquipmentEF(
                       e.EquipmentId,
                       e.Type.ToString(),
                       e.IsInMaintenance

                    );
            } catch (Exception ex) {

                throw new MapException("MapEquipment - MapToDB");
            }
        }
        public static EquipmentType MapStringToEquipmentType(string equipmentType) {

            Enum.TryParse(equipmentType, out EquipmentType result);
                return result;
                
        }

    }
}
