using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Mappers;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Repositories {
    public class EquipmentRepository : IEquipmentRepository {

        private FitnessManagementContext ctx;

        public EquipmentRepository(string connectioString) {
            this.ctx = new FitnessManagementContext(connectioString);
        }
        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public void AddEquipment(Equipment equipment) {
            try {
                ctx.equipment.Add(MapEquipment.MapToDB(equipment));
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepoException("EquipmentRepo - AddEquipment", ex);
            }
        }

        public IEnumerable<Equipment> GetAllEquipment() {
            try {
                return ctx.equipment.Select(x => MapEquipment.MapToDomain(x)).ToList();
            } catch (Exception ex) {
                throw new RepoException("EquipmentRepo - GetAllEquipment", ex);
            }
        }

        public IEnumerable<Equipment> GetAvailableEquipment() {
            try {
                return ctx.equipment
                    .Where(x => !x.IsInMaintenance) 
                    .Select(x => MapEquipment.MapToDomain(x))
                    .ToList();
            } catch (Exception ex) {
                throw new RepoException("EquipmentRepo - GetAvailableEquipment", ex);
            }
        }

        public Equipment GetEquipment(int id) {
            try {
   
                return MapEquipment.MapToDomain(ctx.equipment.Where(x => x.EquipmentId == id).AsNoTracking().FirstOrDefault());
            } catch (Exception ex) {
                throw new RepoException("EquipmentRepo - GetEquipment", ex);
            }
        }

        public bool IsEquipment(int id) {
            try {
                return ctx.equipment.Any(x => x.EquipmentId == id);
            } catch (Exception ex) {
                throw new RepoException("EquipmentRepo - IsEquipment", ex);
            }
        }

        public void SetMaintenance(int equipmentId, bool isInMaintenance) {
            try {
                EquipmentEF equipmentEF = MapEquipment.MapToDB(GetEquipment(equipmentId));
                equipmentEF.IsInMaintenance = isInMaintenance;
                ctx.equipment.Update(equipmentEF);
                SaveAndClear();
            } catch (Exception ex) {
                throw new RepoException("EquipmentRepo - SetMaintenance", ex);
            }
        }
    }
}
