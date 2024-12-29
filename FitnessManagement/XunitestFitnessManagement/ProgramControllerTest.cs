using FitnessBL.Models;
using FitnessManagement.API.Controllers;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.BL.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace XunitestFitnessManagement {
    public class ProgramControllerTest {
        private readonly Mock<IProgramRepository> mockProgramRepo;
        private readonly ProgramService programService;
        private readonly ProgramController programController;

        public ProgramControllerTest() {
            // Mock repositories
            mockProgramRepo = new Mock<IProgramRepository>();

            // Create services with mocked repositories
            programService = new ProgramService(mockProgramRepo.Object);

            // Create the controller with the real services
            programController = new ProgramController(programService);
        }

        [Fact]
        public void GET_UnknownProgramCode_ReturnNotFound() {
            // Arrange
            mockProgramRepo.Setup(repo => repo.GetProgramByProgramCode("unknown"))
                .Throws(new ProgramException("Program doesn't exist."));

            // Act
            var result = programController.Get("unknown");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectProgramCode_ReturnOkResult() {
            // Arrange
            var program = new Program("FIT101", "Fitness Basics", DateTime.Now.AddDays(1), Program.ProgramTarget.beginner, 20);
            mockProgramRepo.Setup(repo => repo.GetProgramByProgramCode("FIT101")).Returns(program);
            mockProgramRepo.Setup(repo => repo.IsProgram("FIT101")).Returns(true);
            // Act
            var result = programController.Get("FIT101");

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectProgramCode_ReturnCorrectProgram() {
            // Arrange
            var expectedProgram = new Program("FIT101", "Fitness Basics", DateTime.Now.AddDays(1), Program.ProgramTarget.beginner, 20);
            mockProgramRepo.Setup(repo => repo.GetProgramByProgramCode("FIT101")).Returns(expectedProgram);
            mockProgramRepo.Setup(repo => repo.IsProgram("FIT101")).Returns(true);
            // Act
            var result = programController.Get("FIT101").Result as OkObjectResult;
            var returnedProgram = result.Value as Program;

            // Assert
            Assert.NotNull(returnedProgram);
            Assert.Equal("FIT101", returnedProgram.ProgramCode);
            Assert.Equal("Fitness Basics", returnedProgram.Name);
            Assert.Equal(Program.ProgramTarget.beginner, returnedProgram.Target);
            Assert.Equal(expectedProgram.StartDate, returnedProgram.StartDate);
            Assert.Equal(20, returnedProgram.MaxMembers);
        }




        [Fact]
        public void POST_ValidProgram_ReturnsCreatedAtAction() {
            // Arrange
            var program = new FitnessManagement.BL.Models.Program {
                ProgramCode = "FIT101",
                Name = "Fitness Basics",
                StartDate = DateTime.Now.AddDays(1),
                Target = FitnessManagement.BL.Models.Program.ProgramTarget.beginner,
                MaxMembers = 20
            };

            // Act
            var response = programController.Post(program);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public void POST_ValidProgram_ReturnsCorrectItem() {
            // Arrange
            var program = new FitnessManagement.BL.Models.Program {
                ProgramCode = "FIT101",
                Name = "Fitness Basics",
                StartDate = DateTime.Now.AddDays(1),
                Target = FitnessManagement.BL.Models.Program.ProgramTarget.beginner,
                MaxMembers = 20
            };

            // Act
            var response = programController.Post(program).Result as CreatedAtActionResult;
            var resultItem = response.Value as FitnessManagement.BL.Models.Program;

            // Assert
            Assert.IsType<FitnessManagement.BL.Models.Program>(resultItem);
            Assert.Equal(program.ProgramCode, resultItem.ProgramCode);
            Assert.Equal(program.Name, resultItem.Name);
            Assert.Equal(program.StartDate, resultItem.StartDate);
            Assert.Equal(program.Target, resultItem.Target);
            Assert.Equal(program.MaxMembers, resultItem.MaxMembers);
        }

        [Fact]
        public void POST_InvalidProgram_ThrowsException_ReturnsBadRequest() {
            // Arrange
            var invalidProgram = new FitnessManagement.BL.Models.Program {
                ProgramCode = "INVALID", // Te lang of niet toegestaan
                Name = "Test",
                StartDate = DateTime.Now.AddDays(1),
                Target = FitnessManagement.BL.Models.Program.ProgramTarget.beginner,
                MaxMembers = 20
            };

            // Simuleer een exception in de AddProgram-functie
            mockProgramRepo.Setup(repo => repo.AddProgram(It.IsAny<FitnessManagement.BL.Models.Program>()))
                .Throws(new ProgramException("Simulated exception: Invalid Program Code"));

            // Act
            var response = programController.Post(invalidProgram).Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal("Simulated exception: Invalid Program Code",
                (response as BadRequestObjectResult).Value);
        }



        [Fact]
        public void PUT_ValidProgram_ReturnsNoContent() {
            // Arrange
            var program = new FitnessManagement.BL.Models.Program {
                ProgramCode = "FIT101",
                Name = "Fitness Basics",
                StartDate = DateTime.Now.AddDays(10),
                Target = FitnessManagement.BL.Models.Program.ProgramTarget.beginner,
                MaxMembers = 20
            };

            // Mock repository behavior
            mockProgramRepo.Setup(repo => repo.IsProgram("FIT101")).Returns(true);
            mockProgramRepo.Setup(repo => repo.UpdateProgram(It.IsAny<FitnessManagement.BL.Models.Program>()));

            // Act
            var response = programController.Put("FIT101", program);

            // Assert
            Assert.IsType<NoContentResult>(response);
            mockProgramRepo.Verify(repo => repo.UpdateProgram(It.IsAny<FitnessManagement.BL.Models.Program>()), Times.Once);
        }

        [Fact]
        public void PUT_InvalidProgram_ReturnsBadRequest() {
            // Arrange
            var program = new FitnessManagement.BL.Models.Program {
                ProgramCode = "WRONG101",
                Name = "Invalid Program",
                StartDate = DateTime.Now.AddDays(10),
                Target = FitnessManagement.BL.Models.Program.ProgramTarget.beginner,
                MaxMembers = 20
            };

            // Act
            var response = programController.Put("DIFFERENT_CODE", program);

            // Assert
            Assert.IsType<BadRequestResult>(response);
            mockProgramRepo.Verify(repo => repo.UpdateProgram(It.IsAny<FitnessManagement.BL.Models.Program>()), Times.Never);
        }

        [Fact]
        public void PUT_ProgramNotExists_ReturnsNotFound() {
            // Arrange
            var program = new FitnessManagement.BL.Models.Program {
                ProgramCode = "FIT101",
                Name = "Fitness Basics",
                StartDate = DateTime.Now.AddDays(10),
                Target = FitnessManagement.BL.Models.Program.ProgramTarget.beginner,
                MaxMembers = 20
            };

            mockProgramRepo.Setup(repo => repo.IsProgram("FIT101")).Returns(false);

            // Act
            var response = programController.Put("FIT101", program);

            // Assert
            Assert.IsType<ConflictObjectResult>(response);
            mockProgramRepo.Verify(repo => repo.UpdateProgram(It.IsAny<FitnessManagement.BL.Models.Program>()), Times.Never);
        }

    }
}
