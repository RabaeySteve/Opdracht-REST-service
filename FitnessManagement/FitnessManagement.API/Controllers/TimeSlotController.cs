//using FitnessManagement.API.DTO_s;
//using FitnessManagement.API.Mapper;
//using FitnessManagement.BL.Exceptions;
//using FitnessManagement.BL.Models;
//using FitnessManagement.BL.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace FitnessManagement.API.Controllers {
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TimeSlotController : ControllerBase {
//        private TimeSlotService TimeSlotService;

//        public TimeSlotController(TimeSlotService timeSlotRepo) {
//            this.TimeSlotService = timeSlotRepo;
//        }


//        public ActionResult<List<TimeSlot>> GetAll() {
//            try {
//                return TimeSlotService.GetAllTimeSlots();
//            } catch (MemberException ex) {
//                return NotFound(ex.Message);
//            }
//        }
//    }
//}
