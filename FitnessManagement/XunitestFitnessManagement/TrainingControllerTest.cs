using FitnessBL.Models;
using FitnessManagement.API.Controllers;
using FitnessManagement.API.DTO_s;
using FitnessManagement.BL.DTO_s.DTOModels;
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
    public class TrainingControllerTest {
        private readonly Mock<ITrainingRepository> mockTrainingRepo;
        private readonly TrainingService trainingService;
        private readonly TrainingController trainingController;

        public TrainingControllerTest() {
            mockTrainingRepo = new Mock<ITrainingRepository>();

            trainingService = new TrainingService(mockTrainingRepo.Object);

            trainingController = new TrainingController(trainingService);
        }
        [Fact]
        public void CreateRunningSession_ValidData_ReturnsRunningSession() {
          
            var member = new Member { MemberId = 1 };
            var details = new List<RunningSessionDetail> {
            new RunningSessionDetail(1, 1, 60, 12.5),
            new RunningSessionDetail(1, 2, 60, 11.0)
             };

            var runningSession = new RunningSession {
                TrainingId = 1,
                Member = member,
                Date = DateTime.Now.AddDays(-1),
                Duration = 60,
                AvgSpeed = 12.0,
                Details = details
            };

            Assert.NotNull(runningSession);
            Assert.Equal(1, runningSession.TrainingId);
            Assert.Equal(member, runningSession.Member);
            Assert.Equal(60, runningSession.Duration);
            Assert.Equal(12.0, runningSession.AvgSpeed);
            Assert.Equal(2, runningSession.Details.Count);
        }
        [Fact]
        public void CreateCyclingSession_ValidData_ReturnsCyclingSession() {
            var member = new Member { MemberId = 1 };

            var cyclingSession = new CyclingSession {
                TrainingId = 1,
                Member = member,
                Date = DateTime.Now.AddDays(-1),
                Duration = 120,
                AvgWatt = 180,
                MaxWatt = 220,
                AvgCadence = 85,
                MaxCadence = 95,
                Comment = "Great session!",
                Type = CyclingSession.CyclingTrainingType.endurance
            };
            Assert.NotNull(cyclingSession);
            Assert.Equal(1, cyclingSession.TrainingId);
            Assert.Equal(member, cyclingSession.Member);
            Assert.Equal(120, cyclingSession.Duration);
            Assert.Equal(180, cyclingSession.AvgWatt);
            Assert.Equal(220, cyclingSession.MaxWatt);
            Assert.Equal(85, cyclingSession.AvgCadence);
            Assert.Equal(95, cyclingSession.MaxCadence);
            Assert.Equal("Great session!", cyclingSession.Comment);
            Assert.Equal(CyclingSession.CyclingTrainingType.endurance, cyclingSession.Type);
        }
        [Fact]
        public void GetAllMonth_ValidInput_ReturnsMappedSessions() {
            var memberId = 1;
            var year = 2023;
            var month = 12;

            var sessions = new List<TrainingSessionBase> {
        new RunningSession {
            TrainingId = 1,
            Member = new Member { MemberId = 1 },
            Date = new DateTime(2023, 12, 10),
            Duration = 60
        },
        new CyclingSession {
            TrainingId = 2,
            Member = new Member { MemberId = 1 },
            Date = new DateTime(2023, 12, 12),
            Duration = 120
        }
    };

            mockTrainingRepo.Setup(repo => repo.GetSessionsForCustomerMonth(memberId, year, month)).Returns(sessions);

            var result = trainingController.GetAllMonth(memberId, year, month).Result as OkObjectResult;
            var mappedSessions = result.Value as List<TrainingMapped>;

            Assert.NotNull(mappedSessions);
            Assert.Equal(2, mappedSessions.Count);
            Assert.Equal(1, mappedSessions[0].TrainingId);
            Assert.Equal(TrainingSessionType.Running, mappedSessions[0].TrainingSessionType);
            Assert.Equal(2, mappedSessions[1].TrainingId);
            Assert.Equal(TrainingSessionType.Cycling, mappedSessions[1].TrainingSessionType);
        }
        [Fact]
        public void GetAllMonth_InvalidInput_ReturnsBadRequest() {
            var memberId = 0;
            var year = 2023;
            var month = 12;

            var result = trainingController.GetAllMonth(memberId, year, month).Result;

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Ongeldige inputparameters.", (result as BadRequestObjectResult).Value);
        }
        [Fact]
        public void GetAllMonth_ValidInput_NoSessionsFound_ReturnsNotFound() {
            var memberId = 1;
            var year = 2023;
            var month = 12;

            mockTrainingRepo.Setup(repo => repo.GetSessionsForCustomerMonth(memberId, year, month)).Returns(new List<TrainingSessionBase>());

            var result = trainingController.GetAllMonth(memberId, year, month).Result;

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Geen sessies gevonden voor klant ID {memberId} in {year}-{month}.",
                         (result as NotFoundObjectResult).Value);
        }
      


    }
}
