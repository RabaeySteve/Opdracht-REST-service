using FitnessManagement.API.Controllers;
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
    public class EquipmentControllerTest {
        private readonly Mock<IEquipmentRepository> mockEquipmentRepo;
        private readonly EquipmentService equipmentService;
        private readonly EquipmentController equipmentController;

        public EquipmentControllerTest() {
            mockEquipmentRepo = new Mock<IEquipmentRepository>();

            equipmentService = new EquipmentService(mockEquipmentRepo.Object);
            equipmentController = new EquipmentController(equipmentService);
        }

        [Fact]
        public void GET_UnknownEquipmentId_ReturnNotFound() {
            mockEquipmentRepo.Setup(repo => repo.GetEquipment(1))
                .Throws(new EquipmentException("Equipment doesn't exist."));

            ActionResult<Equipment> result = equipmentController.Get(1);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectEquipmentId_ReturnOkResult() {
        
            mockEquipmentRepo.Setup(repo => repo.IsEquipment(1)).Returns(true);
            mockEquipmentRepo.Setup(repo => repo.GetEquipment(1))
                .Returns(new Equipment {
                    EquipmentId = 1,
                    Type = Equipment.EquipmentType.treadmill,
                    IsInMaintenance = false
                });

        
            var result = equipmentController.Get(1);

     
            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public void GET_CorrectEquipmentId_ReturnCorrectEquipment() {
         
            Equipment expectedEquipment = new Equipment {
                EquipmentId = 1,
                Type = Equipment.EquipmentType.treadmill,
                IsInMaintenance = false
            };
            mockEquipmentRepo.Setup(repo => repo.IsEquipment(1)).Returns(true);
            mockEquipmentRepo.Setup(repo => repo.GetEquipment(1)).Returns(expectedEquipment);

           
            var result = equipmentController.Get(1).Result as OkObjectResult;

          
            Assert.IsType<Equipment>(result.Value);
            Assert.Equal(1, (result.Value as Equipment).EquipmentId);
            Assert.Equal(Equipment.EquipmentType.treadmill, (result.Value as Equipment).Type);
            Assert.False((result.Value as Equipment).IsInMaintenance);
        }




        [Fact]
        public void POST_ValidEquipment_ReturnsCreatedAtAction() {
            Equipment equipment = new Equipment {
                EquipmentId = 1,
                Type = Equipment.EquipmentType.treadmill,
                IsInMaintenance = false
            };

            ActionResult<Equipment> response = equipmentController.Post(equipment);

            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public void POST_ValidEquipment_ReturnsCorrectItem() {

            Equipment equipment = new Equipment {
                EquipmentId = 1,
                Type = Equipment.EquipmentType.treadmill,
                IsInMaintenance = false
            };

            CreatedAtActionResult? response = equipmentController.Post(equipment).Result as CreatedAtActionResult;
            Equipment? resultItem = response.Value as Equipment;
            
            Assert.IsType<Equipment>(resultItem);
            Assert.Equal(equipment.EquipmentId, resultItem.EquipmentId);
            Assert.Equal(equipment.Type, resultItem.Type);
            Assert.Equal(equipment.IsInMaintenance, resultItem.IsInMaintenance);
        }

        [Fact]
        public void POST_InvalidEquipment_ThrowsException_ReturnsBadRequest() {

            Equipment invalidEquipment = new Equipment {
                EquipmentId = 1,
                Type = Equipment.EquipmentType.treadmill, 
                IsInMaintenance = false
            };

          
            mockEquipmentRepo.Setup(repo => repo.AddEquipment(It.IsAny<Equipment>()))
                .Throws(new EquipmentException("Simulated exception: Invalid Equipment Type"));


            ActionResult? response = equipmentController.Post(invalidEquipment).Result;

          
            Assert.IsType<BadRequestObjectResult>(response);
          
        }


        [Fact]
        public void PUT_ValidEquipment_ReturnsNoContent() {
            EquipmentDTO equipmentDTO = new EquipmentDTO {
                EquipmentId = 1,
                IsInMaintenance = true
            };

            mockEquipmentRepo.Setup(repo => repo.IsEquipment(1)).Returns(true);
            mockEquipmentRepo.Setup(repo => repo.SetMaintenance(1, true));

            IActionResult response = equipmentController.Put(1, equipmentDTO);

            Assert.IsType<NoContentResult>(response);
            mockEquipmentRepo.Verify(repo => repo.SetMaintenance(1, true), Times.Once);
        }

        [Fact]
        public void PUT_InvalidEquipment_ReturnsBadRequest() {
            EquipmentDTO equipmentDTO = new EquipmentDTO {
                EquipmentId = 1,
                IsInMaintenance = true
            };

            IActionResult response = equipmentController.Put(2, equipmentDTO);

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal("EquipmentId in the body is not the same as the routeID.",
                (response as BadRequestObjectResult).Value);
            mockEquipmentRepo.Verify(repo => repo.SetMaintenance(It.IsAny<int>(), It.IsAny<bool>()), Times.Never);
        }

        [Fact]
        public void PUT_EquipmentNotExists_ReturnsNotFound() {
            EquipmentDTO equipmentDTO = new EquipmentDTO {
                EquipmentId = 1,
                IsInMaintenance = true
            };

            mockEquipmentRepo.Setup(repo => repo.IsEquipment(1)).Returns(false);
            IActionResult response = equipmentController.Put(1, equipmentDTO);

            Assert.IsType<ConflictObjectResult>(response);
           
        }


    }
}
