using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface IEquipmentRepository {
        IEnumerable<Equipment> GetAllEquipment();
        Equipment GetEquipment(int id);
        bool IsEquipment(int id);
        void AddEquipment(Equipment equipment);
       
        void SetMaintenance(int equipmentId, bool IsInMaintenance);
        IEnumerable<Equipment> GetAvailableEquipment();

    }
}
