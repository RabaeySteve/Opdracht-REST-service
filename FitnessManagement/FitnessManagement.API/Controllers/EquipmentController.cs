using FitnessBL.Models;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Mapper;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Models;
using FitnessManagement.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase {
        private EquipmentService EquipmentRepo;

        public EquipmentController(EquipmentService equipmentRepo) {
            this.EquipmentRepo = equipmentRepo;

        }

        [HttpGet("{id}")]
        public ActionResult<Equipment> Get(int id) {
            try {
                return EquipmentRepo.GetEquipment(id);
            } catch (MemberException ex) {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Equipment> Post(Equipment equipment) {
            EquipmentRepo.AddEquipment(equipment);
            return CreatedAtAction(nameof(Get), new { id = equipment.EquipmentId }, equipment);
        }
        [HttpPut]
        public IActionResult Put(int id, [FromBody] EquipmentDTO equipment) {
            if (equipment == null || equipment.EquipmentId != id) {
                return BadRequest();
            }
            
            EquipmentRepo.SetMaintenance(equipment.EquipmentId, equipment.IsInMaintenance);
            return new NoContentResult();
        }
    }
}
