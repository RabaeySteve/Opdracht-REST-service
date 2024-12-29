using FitnessBL.Models;
using FitnessManagement.API.Controllers;
using FitnessManagement.API.DTO_s;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.BL.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace XunitestFitnessManagement {
    public class MemberControllerTest {
        private readonly Mock<IMemberRepository> mockMemberRepo;
        private readonly Mock<IReservationRepository> mockReservationRepo;
        private readonly MemberService memberService;
        private readonly ReservationService reservationService;
        private readonly MemberController memberController;

        public MemberControllerTest() {
            mockMemberRepo = new Mock<IMemberRepository>();
            mockReservationRepo = new Mock<IReservationRepository>();
            memberService = new MemberService(mockMemberRepo.Object);
            reservationService = new ReservationService(mockReservationRepo.Object);

            memberController = new MemberController(memberService, reservationService);
        }

        [Fact]
        public void GET_UnknownID_ReturnNotFound() {
            mockMemberRepo.Setup(repo => repo.GetMember(2))
                .Throws(new MemberException("Member doesn't exist."));

            var result = memberController.Get(2);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectID_ReturnOkResult() {
            mockMemberRepo.Setup(repo => repo.IsMember(2)).Returns(true);
            mockMemberRepo.Setup(repo => repo.GetMember(2))
                .Returns(new Member(2, "Carl", "De Vught", "carl.devught@google.com", "Adegem", new DateOnly(1960, 1, 12), null, Member.MemberType.Silver, null));



            var result = memberController.Get(2);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectID_ReturnCorrectMember() {
            Member expectedMember = new Member(2, "Carl", "De Vught", "carl.devught@google.com", "Adegem", new DateOnly(1960, 1, 12), null, Member.MemberType.Silver, null);
            mockMemberRepo.Setup(repo => repo.IsMember(2)).Returns(true);
            mockMemberRepo.Setup(repo => repo.GetMember(2)).Returns(expectedMember);

            var result = memberController.Get(2).Result as OkObjectResult;

            Assert.IsType<MemberDTO>(result.Value);
            Assert.Equal(2, (result.Value as MemberDTO).MemberId);
            Assert.Equal("Carl", (result.Value as MemberDTO).FirstName);
            Assert.Equal("De Vught", (result.Value as MemberDTO).LastName);
            Assert.Equal("carl.devught@google.com", (result.Value as MemberDTO).Email);
            Assert.Equal("Adegem", (result.Value as MemberDTO).Address);
            Assert.Equal(new DateOnly(1960, 1, 12), (result.Value as MemberDTO).Birthday);
            Assert.Equal("Silver", (result.Value as MemberDTO).Type);
        }

        [Fact]
        public void GET_MemberPrograms_UnknownID_ReturnNotFound() {

            mockMemberRepo.Setup(repo => repo.GetMember(2))
                .Throws(new MemberException("Member doesn't exist."));


            var result = memberController.GetMemberPrograms(2);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GET_MemberPrograms_CorrectID_ReturnOkResult() {
            // Arrange
            Member member = new Member(2, "Carl", "De Vught", "carl.devught@google.com", "Adegem",
                new DateOnly(1960, 1, 12), new List<string> { "Running", "Cycling" }, Member.MemberType.Silver,
                new List<FitnessManagement.BL.Models.Program> {
            new FitnessManagement.BL.Models.Program { ProgramCode = "car", Name = "Cardio Training",
                Target = FitnessManagement.BL.Models.Program.ProgramTarget.beginner, StartDate = new DateTime(2025, 11, 15), MaxMembers = 20 }

                });
            mockMemberRepo.Setup(repo => repo.GetMember(2)).Returns(member);


            mockMemberRepo.Setup(repo => repo.IsMember(2)).Returns(true);
            var result = memberController.GetMemberPrograms(2);


            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_MemberPrograms_CorrectID_ReturnCorrectPrograms() {

            Member member = new Member(2, "Carl", "De Vught", "carl.devught@google.com", "Adegem",
                new DateOnly(1960, 1, 12), new List<string> { "Running", "Cycling" }, Member.MemberType.Silver,
                new List<FitnessManagement.BL.Models.Program> {
             new FitnessManagement.BL.Models.Program { ProgramCode = "car", Name = "Cardio Training",
                Target = FitnessManagement.BL.Models.Program.ProgramTarget.beginner, StartDate = new DateTime(2025, 11, 15), MaxMembers = 20 }

                });
            mockMemberRepo.Setup(repo => repo.GetMember(2)).Returns(member);


            mockMemberRepo.Setup(repo => repo.IsMember(2)).Returns(true);
            var result = memberController.GetMemberPrograms(2).Result as OkObjectResult;
            var returnedDto = result.Value as MemberProgramsDTO;

            Assert.NotNull(returnedDto);
            Assert.Equal(2, returnedDto.MemberId);
            Assert.Equal(1, returnedDto.ProgramsList.Count);

            var program = returnedDto.ProgramsList[0];
            Assert.Equal("car", program.ProgramCode);
            Assert.Equal("Cardio Training", program.Name);
            Assert.Equal(FitnessManagement.BL.Models.Program.ProgramTarget.beginner, program.Target);
            Assert.Equal(new DateTime(2025, 11, 15), program.StartDate);
            Assert.Equal(20, program.MaxMembers);

        }


        [Fact]
        public void GET_MemberReservations_UnknownID_ReturnsNotFound() {

            mockReservationRepo.Setup(repo => repo.GetReservationMember(2))
                .Throws(new ReservationException("Member doesn't exist."));


            var result = memberController.GetMemberReservations(2);


            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GET_MemberReservations_NoReservations_ReturnsNotFound() {

            mockReservationRepo.Setup(repo => repo.GetReservationMember(2)).Returns(new List<Reservation>());


            var result = memberController.GetMemberReservations(2);


            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GET_MemberReservations_CorrectID_ReturnsOkResult() {

            mockReservationRepo.Setup(repo => repo.GetReservationMember(2)).Returns(new List<Reservation> {
        new Reservation(
            new DateOnly(2023, 12, 30),
            1,
            101,
            new Member(2, "John", "Doe", "john.doe@example.com", "Main Street 1", new DateOnly(1985, 5, 10), new List<string> { "Running" }, Member.MemberType.Gold, new List<Program>())
        )
    });


            var result = memberController.GetMemberReservations(2);


            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_MemberReservations_CorrectID_ReturnsCorrectReservations() {
      
            var member = new Member(2, "John", "Doe", "john.doe@example.com", "Main Street 1", new DateOnly(1985, 5, 10), new List<string> { "Running" }, Member.MemberType.Gold, new List<Program>());
            var reservation = new Reservation(new DateOnly(2023, 12, 30), 1, 101, member) {
                TimeSLotEquipment = new Dictionary<int, Equipment> {
            { 1, new Equipment { EquipmentId = 1, Type = Equipment.EquipmentType.bike, IsInMaintenance = false } },
            { 2, new Equipment { EquipmentId = 2, Type =  Equipment.EquipmentType.treadmill, IsInMaintenance = true } }
        }
            };
            mockReservationRepo.Setup(repo => repo.GetReservationMember(2)).Returns(new List<Reservation> { reservation });

         
            var result = memberController.GetMemberReservations(2).Result as OkObjectResult;
            var returnedReservations = result.Value as List<ReservationGetDTO>;

       
            Assert.NotNull(returnedReservations);
            Assert.Single(returnedReservations);
            Assert.Equal(101, returnedReservations[0].ReservationId);
            Assert.Equal(2, returnedReservations[0].MemberId);
            Assert.Equal("John", returnedReservations[0].FirstName);
            Assert.Equal("Doe", returnedReservations[0].LastName);
            Assert.Equal("john.doe@example.com", returnedReservations[0].Email);
            Assert.Equal(2, returnedReservations[0].Reservations.Count);

            Assert.Equal(1, returnedReservations[0].Reservations[0].TimeSlot.TimeSlotId);
            Assert.Equal("bike", returnedReservations[0].Reservations[0].Equipment.EquipmentType);

            Assert.Equal(2, returnedReservations[0].Reservations[1].TimeSlot.TimeSlotId);
            Assert.Equal("treadmill", returnedReservations[0].Reservations[1].Equipment.EquipmentType);

        }


        [Fact]
        public void POST_ValidObject_ReturnsCreatedAtAction() {
            
            var memberDTO = new MemberDTO {
                MemberId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Address = "123 Main Street",
                Birthday = new DateOnly(1990, 1, 1),
                Interests = new List<string> { "Running", "Cycling" },
                Type = "Gold"
            };


            var response = memberController.Post(memberDTO);

            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public void POST_ValidObject_ReturnsCorrectItem() {
         
            var memberDTO = new MemberDTO {
                MemberId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Address = "123 Main Street",
                Birthday = new DateOnly(1990, 1, 1),
                Interests = new List<string> { "Running", "Cycling" },
                Type = "Bronze"
            };

          
            var response = memberController.Post(memberDTO).Result as CreatedAtActionResult;
            var resultItem = response.Value as MemberDTO;

           
            Assert.IsType<MemberDTO>(resultItem);
            Assert.Equal(memberDTO.MemberId, resultItem.MemberId);
            Assert.Equal(memberDTO.FirstName, resultItem.FirstName);
            Assert.Equal(memberDTO.LastName, resultItem.LastName);
            Assert.Equal(memberDTO.Email, resultItem.Email);
            Assert.Equal(memberDTO.Address, resultItem.Address);
            Assert.Equal(memberDTO.Birthday, resultItem.Birthday);
            Assert.Equal(memberDTO.Interests, resultItem.Interests);
            Assert.Equal(memberDTO.Type, resultItem.Type);
        }

        [Fact]
        public void POST_InvalidObject_ReturnsBadRequest() {
          
            var invalidMemberDTO = new MemberDTO {
                MemberId = 1,
                FirstName = "Jhon",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Address = "123 Main Street",
                Birthday = new DateOnly(1990, 1, 1),
                Interests = null,
                Type = "Gold"
            };

            memberController.ModelState.AddModelError("Email", "Invalid email format");

            var response = memberController.Post(invalidMemberDTO).Result;

        
            Assert.IsType<BadRequestObjectResult>(response);
        }
        [Fact]
        public void POST_AddProgram_ValidObject_ReturnsCreatedAtAction() {
          
            var programDTO = new MemberAddProgramDTO {
                MemberId = 1,
                ProgramCode = "car"
            };

            mockMemberRepo.Setup(repo => repo.IsMember(1)).Returns(true);

        
            mockMemberRepo.Setup(repo => repo.IsProgram("car")).Returns(true);
            var response = memberController.Post(programDTO).Result;

          
            Assert.IsType<CreatedAtActionResult>(response);
            var result = response as CreatedAtActionResult;
            Assert.NotNull(result);
            Assert.Equal(nameof(memberController.Get), result.ActionName);
            Assert.Equal(1, result.RouteValues["id"]);
            Assert.Equal(programDTO, result.Value);
        }
        [Fact]
        public void POST_AddProgram_InvalidObject_ReturnsBadRequest() {
          
            var invalidProgramDTO = new MemberAddProgramDTO {
                MemberId = 1,
                ProgramCode = null
            };

            memberController.ModelState.AddModelError("ProgramCode", "ProgramCode is required");

           
            var response = memberController.Post(invalidProgramDTO).Result;


            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void POST_AddProgram_UnknownMember_ReturnsNotFound() {
          
            var programDTO = new MemberAddProgramDTO {
                MemberId = 99, 
                ProgramCode = "car"
            };

            mockMemberRepo.Setup(repo => repo.IsMember(99)).Returns(false);

          
            var response = memberController.Post(programDTO).Result;

         
            Assert.IsType<NotFoundObjectResult>(response);
        }
       
        [Fact]
        public void PUT_ValidObject_ReturnsNoContent() {
           
            var memberDTO = new MemberDTO {
                MemberId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Address = "123 Main Street",
                Birthday = new DateOnly(1990, 1, 1),
                Interests = new List<string> { "Running", "Cycling" },
                Type = "Gold"
            };

            mockMemberRepo.Setup(repo => repo.IsMember(1)).Returns(true);
            mockMemberRepo.Setup(repo => repo.UpdateMember(It.IsAny<Member>()));

           
            var response = memberController.Put(1, memberDTO);

            
            Assert.IsType<NoContentResult>(response);
            mockMemberRepo.Verify(repo => repo.UpdateMember(It.IsAny<Member>()), Times.Once);
        }



    }
}
