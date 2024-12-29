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
        private EquipmentService EquipmentService;

        public EquipmentController(EquipmentService equipmentService) {
            this.EquipmentService = equipmentService;

        }

        [HttpGet("{id}")]
        public ActionResult<Equipment> Get(int id) {
            try {
                Equipment equipment = EquipmentService.GetEquipment(id);
                return Ok(equipment);
            } catch (Exception ex) {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Equipment> Post(Equipment equipment) {
            try {
                EquipmentService.AddEquipment(equipment);
                return CreatedAtAction(nameof(Get), new { id = equipment.EquipmentId }, equipment);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpPut]
        public IActionResult Put(int id, [FromBody] EquipmentDTO equipment) {
            if (equipment == null || equipment.EquipmentId != id) {
                return BadRequest("EquipmentId in the body is not the same as the routeID.");
            }
            try {
                EquipmentService.SetMaintenance(equipment.EquipmentId, equipment.IsInMaintenance);
                return new NoContentResult();
            } catch (Exception ex) {
                return Conflict(new { message = ex.Message });
            }
          
        }
    }
}
