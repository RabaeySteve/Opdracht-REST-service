using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Services {
    public class EquipmentService {
        private IEquipmentRepository repo;


        public EquipmentService(IEquipmentRepository repo) {
            this.repo = repo;
        }

        public IEnumerable<Equipment> GetAllEquipment() {
            try {
                return repo.GetAllEquipment();
            } catch (Exception ex) {

                throw new EquipmentException("GetAllEquipment", ex);
            }
        }
        public Equipment GetEquipment(int id) {
            try {
                if (!repo.IsEquipment(id)) {
                    throw new EquipmentException("GetEquipment - Equipment does not exist");
                }
                return repo.GetEquipment(id);
            } catch (Exception ex) {

                throw new EquipmentException("GetEquipment", ex);
            }
        }
        public Equipment AddEquipment(Equipment equipment) {
            try {

                repo.AddEquipment(equipment);
                return equipment;

            } catch (Exception ex) {

                throw new EquipmentException("AddEquipment", ex);
            }
        }

        public bool IsEquipment(int id) {
            try {
                return repo.IsEquipment(id);
            } catch (Exception ex) {

                throw new EquipmentException("IsEquipment", ex);
            }
        }
        public void SetMaintenance(int equipmentId, bool isInMaintenance) {
            try {
                if (!repo.IsEquipment(equipmentId)) {
                    throw new EquipmentException($"SetMaintenance - Equipment with ID {equipmentId} does not exist.");
                }

                repo.SetMaintenance(equipmentId, isInMaintenance);
            } catch (EquipmentException) {
               
                throw;
            } catch (Exception ex) {
                
                throw new EquipmentException($"SetMaintenance - Unexpected error occurred for Equipment ID {equipmentId}.", ex);
            }
        }

        public IEnumerable<Equipment> GetAvailableEquipment() {
            try {
                return repo.GetAvailableEquipment();
            } catch (Exception ex) {

                throw new EquipmentException("GetAvailableEquipment", ex);
            }
        }
    }
}
