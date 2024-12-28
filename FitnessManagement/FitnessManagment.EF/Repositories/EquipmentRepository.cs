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

        public EquipmentRepository(string connectionString) {
            this.ctx = new FitnessManagementContext(connectionString);
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
   
                return MapEquipment.MapToDomain(ctx.equipment.Where(x => x.EquipmentId == id)
                    .AsNoTracking().FirstOrDefault());
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
            //1: Markeer het toestel als in onderhoud
            EquipmentEF? equipment = ctx.equipment.FirstOrDefault(e => e.EquipmentId == equipmentId);
            if (equipment == null) throw new Exception("Equipment not found.");
            equipment.IsInMaintenance = isInMaintenance;
            ctx.equipment.Update(equipment);

            if (!isInMaintenance) return;

            //2: Vind alle reserveringen met dit toestel
            List<ReservationEF> reservations = ctx.reservation
                .Where(r => r.Equipment.EquipmentId == equipmentId)
                .Include(r => r.TimeSlot)
                .ToList();

            foreach (ReservationEF? reservation in reservations) {
                //3: Zoek een alternatief toestel
                IEnumerable<Equipment> availableEquipments = GetAvailableEquipmentByType(equipment.Type, reservation.TimeSlot.TimeSlotId, reservation.Date);
                if (availableEquipments.Any()) {
                    // Vervang het toestel in de reservering
                    Equipment newEquipment = availableEquipments.First();
                    UpdateReservationEquipment(reservation.ReservationId, equipmentId, newEquipment);
                } else {
                    string connectionString = ctx.Database.GetDbConnection().ConnectionString;
                    ReservationRepository reservationRepo = new ReservationRepository(connectionString);
                    reservationRepo.DeleteReservation(reservation.ReservationId);
                }
            }
            SaveAndClear();
        }
        public void UpdateReservationEquipment(int reservationId, int oldEquipmentId, Equipment equipment) {
            try {
                string connectionString = ctx.Database.GetDbConnection().ConnectionString;
                ReservationRepository reservationRepo = new ReservationRepository(connectionString);
                ReservationEF? reservationEF = ctx.reservation.Where(r => r.ReservationId == reservationId && r.Equipment.EquipmentId == oldEquipmentId)
                    .Include(x => x.Member).FirstOrDefault();
                Reservation reservation = MapReservation.MapToDomain(reservationEF, ctx);
                reservationRepo.DeleteReservation(reservationId);
                SaveAndClear();
                // Zoek het tijdslot-ID dat vervangen moet worden
                var timeSlotId = reservation.TimeSLotEquipment.FirstOrDefault(kvp => kvp.Value.EquipmentId == oldEquipmentId).Key;

                // Controleer of het tijdslot-ID bestaat
                if (timeSlotId == 0) {
                    throw new RepoException($"Equipment with ID {oldEquipmentId} not found in the reservation.");
                }

                // Vervang het hele Equipment-object in de dictionary
                reservation.TimeSLotEquipment[timeSlotId] = equipment;
              
                if (reservationRepo.IsTimeSlotAvailable(reservation)) {
                    reservationRepo.AddReservation(reservation);
                };
               
            } catch (Exception ex) {
                throw new RepoException("ReservationRepo - UpdateReservationEquipment", ex);
            }
        }
        public IEnumerable<Equipment> GetAvailableEquipmentByType(string equipmentType, int timeSlotId, DateOnly date) {
            try {
               
                return ctx.equipment
                    .Where(e => !e.IsInMaintenance && e.Type == equipmentType) 
                    .Where(e => !ctx.reservation.Any(r => r.Equipment.EquipmentId == e.EquipmentId && r.TimeSlot.TimeSlotId == timeSlotId && r.Date == date)) // Niet gereserveerd op hetzelfde tijdslot
                    .Select(e => MapEquipment.MapToDomain(e))
                    .ToList();
            } catch (Exception ex) {
                throw new RepoException("EquipmentRepo - GetAvailableEquipmentByType", ex);
            }
        }


    }
}
