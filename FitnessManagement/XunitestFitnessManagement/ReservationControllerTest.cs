using FitnessBL.Models;
using FitnessManagement.API.Controllers;
using FitnessManagement.API.DTO_s;
using FitnessManagement.API.Mapper;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.BL.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XunitestFitnessManagement {
    public class ReservationControllerTest {
        private readonly Mock<IReservationRepository> mockReservationRepo;
        private readonly ReservationService reservationService;
        private readonly ReservationController reservationController;

        public ReservationControllerTest() {

            mockReservationRepo = new Mock<IReservationRepository>();

            reservationService = new ReservationService(mockReservationRepo.Object);

            reservationController = new ReservationController(reservationService);
        }

        [Fact]
        public void GET_Reservation_UnknownID_ReturnNotFound() {

            mockReservationRepo.Setup(repo => repo.GetReservation(2))
                .Throws(new ReservationException("Reservation doesn't exist."));

            var result = reservationController.Get(2);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GET_Reservation_CorrectID_ReturnOkResult() {
            var reservation = new Reservation(
                new DateOnly(2023, 12, 29), 1, 101,
                new Member(2, "Carl", "De Vught", "carl.devught@google.com", "Adegem", new DateOnly(1960, 1, 12), null, Member.MemberType.Silver, null)
            );
            mockReservationRepo.Setup(repo => repo.GetReservation(101)).Returns(reservation);
            mockReservationRepo.Setup(repo => repo.IsReservation(101)).Returns(true);

            var result = reservationController.Get(101);

 
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_Reservation_CorrectID_ReturnCorrectReservation() {
      
            var reservation = new Reservation(
                new DateOnly(2023, 12, 29), 1, 101,
                new Member(2, "Carl", "De Vught", "carl.devught@google.com", "Adegem", new DateOnly(1960, 1, 12), null, Member.MemberType.Silver, null)
            );
            mockReservationRepo.Setup(repo => repo.GetReservation(101)).Returns(reservation);
            mockReservationRepo.Setup(repo => repo.IsReservation(101)).Returns(true);

            var result = reservationController.Get(101).Result as OkObjectResult;
            var returnedDto = result.Value as ReservationGetDTO;

            Assert.NotNull(returnedDto);
            Assert.Equal(101, returnedDto.ReservationId);
            Assert.Equal(1, returnedDto.GroupsId);
            Assert.Equal(2, returnedDto.MemberId);
            Assert.Equal("Carl", returnedDto.FirstName);
            Assert.Equal("De Vught", returnedDto.LastName);
            Assert.Equal("carl.devught@google.com", returnedDto.Email);
            Assert.Equal(new DateOnly(2023, 12, 29), returnedDto.Date);
        }

        [Fact]
        public void GET_AvailableTimeSlots_InvalidDate_ReturnsBadRequest() {
            var invalidDate = "invalid-date";

            var result = reservationController.GetAvailableTimeSlots(invalidDate);

            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid date format. Please use 'yyyy-MM-dd'.",
                (result.Result as BadRequestObjectResult)?.Value);
        }


        [Fact]
        public void POST_ValidReservation_ReturnsCreatedAtAction() {
           
            var reservationDTO = new ReservationPostDTO {
                MemberId = 1,
                Date = new DateOnly(2025, 01, 02),
                Reservations = new List<TimeSlotEquipmentDTO> {
                    new TimeSlotEquipmentDTO { TimeSlotId = 1, EquipmentId = 1 },
                    new TimeSlotEquipmentDTO { TimeSlotId = 2, EquipmentId = 2 }
                }
            };
            mockReservationRepo.Setup(repo => repo.IsTimeSlotAvailable(It.IsAny<Reservation>()))
    .Returns(true); 

           
            var response = reservationController.Post(reservationDTO);

           
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public void POST_ValidReservation_ReturnsCorrectItem() {
            
            var reservationDTO = new ReservationPostDTO {
                MemberId = 1,
                Date = new DateOnly(2025, 01, 02),
                Reservations = new List<TimeSlotEquipmentDTO> {
                    new TimeSlotEquipmentDTO { TimeSlotId = 1, EquipmentId = 1 },
                    new TimeSlotEquipmentDTO { TimeSlotId = 2, EquipmentId = 2 }
                }
            };
            mockReservationRepo.Setup(repo => repo.IsTimeSlotAvailable(It.IsAny<Reservation>()))
             .Returns(true); 

           
            var response = reservationController.Post(reservationDTO).Result as CreatedAtActionResult;
            var resultItem = response.Value as ReservationPostDTO;

          
            Assert.IsType<ReservationPostDTO>(resultItem);
            Assert.Equal(reservationDTO.MemberId, resultItem.MemberId);
            Assert.Equal(reservationDTO.Date, resultItem.Date);
            Assert.Equal(reservationDTO.Reservations.Count, resultItem.Reservations.Count);
            Assert.Equal(reservationDTO.Reservations[0].TimeSlotId, resultItem.Reservations[0].TimeSlotId);
            Assert.Equal(reservationDTO.Reservations[0].EquipmentId, resultItem.Reservations[0].EquipmentId);
        }

        [Fact]
        public void POST_InvalidReservation_ReturnsBadRequest() {
          
            var invalidReservationDTO = new ReservationPostDTO {
                MemberId = 1,
                Date = new DateOnly(2025, 01, 02),
                Reservations = null
            };

            reservationController.ModelState.AddModelError("Reservations", "Reservations cannot be null");
            mockReservationRepo.Setup(repo => repo.IsTimeSlotAvailable(It.IsAny<Reservation>()))
             .Returns(true); 
           
            var response = reservationController.Post(invalidReservationDTO).Result;

           
            Assert.IsType<BadRequestObjectResult>(response);
        }







        [Fact]
        public void PUT_ValidReservation_ReturnsNoContent() {
       
            var reservationPutDTO = new ReservationPutDTO {
                ReservationId = 1,
                GroupsId = 1,
                MemberId = 1,
                Date = new DateOnly(2025, 01, 01),
                Reservations = new List<TimeSlotEquipmentDTO> {
            new TimeSlotEquipmentDTO { TimeSlotId = 1, EquipmentId = 101 },
            new TimeSlotEquipmentDTO { TimeSlotId = 2, EquipmentId = 102 }
        }
            };

            mockReservationRepo.Setup(repo => repo.IsReservation(1)).Returns(true);
            mockReservationRepo.Setup(repo => repo.IsTimeSlotAvailable(It.IsAny<Reservation>()))
          .Returns(true);
           
            var result = reservationController.Put(1, reservationPutDTO);

           
            Assert.IsType<NoContentResult>(result);
            mockReservationRepo.Verify(repo => repo.UpdateReservation(It.IsAny<Reservation>()), Times.Once);
        }

        [Fact]
        public void PUT_NonExistentReservation_ReturnsNotFound() {
          
            var reservationPutDTO = new ReservationPutDTO {
                ReservationId = 1,
                GroupsId = 1,
                MemberId = 1,
                Date = new DateOnly(2025, 01, 01),
                Reservations = new List<TimeSlotEquipmentDTO> {
            new TimeSlotEquipmentDTO { TimeSlotId = 1, EquipmentId = 101 },
            new TimeSlotEquipmentDTO { TimeSlotId = 2, EquipmentId = 102 }
        }
            };

            mockReservationRepo.Setup(repo => repo.IsReservation(1)).Returns(false);

        
            var result = reservationController.Put(1, reservationPutDTO);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Reservation with ID 1 does not exist.", (result as NotFoundObjectResult).Value);
            mockReservationRepo.Verify(repo => repo.UpdateReservation(It.IsAny<Reservation>()), Times.Never);
        }

        [Fact]
        public void PUT_NullReservation_ReturnsBadRequest() {
            ReservationPutDTO reservationPutDTO = null;

   
            var result = reservationController.Put(1, reservationPutDTO);

          
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Data cannot be null.", (result as BadRequestObjectResult).Value);
            mockReservationRepo.Verify(repo => repo.UpdateReservation(It.IsAny<Reservation>()), Times.Never);
        }

        [Fact]
        public void PUT_InvalidModelState_ReturnsBadRequest() {
            var invalidReservationPutDTO = new ReservationPutDTO {
                ReservationId = 1,
                GroupsId = 1,
                MemberId = 1,
                Date = new DateOnly(2025, 01,01),
                Reservations = new List<TimeSlotEquipmentDTO> {
            new TimeSlotEquipmentDTO { TimeSlotId = 1, EquipmentId = 101 },
            new TimeSlotEquipmentDTO { TimeSlotId = 2, EquipmentId = 102 }
         }
            };

            reservationController.ModelState.AddModelError("Reservations", "Reservations cannot be null");

            mockReservationRepo.Setup(repo => repo.IsReservation(1)).Returns(true);
            mockReservationRepo.Setup(repo => repo.IsTimeSlotAvailable(It.IsAny<Reservation>()))
        .Returns(true);
            
            var response = reservationController.Put(1, invalidReservationPutDTO);

            Assert.IsType<NoContentResult>(response);
        }

    }
}
