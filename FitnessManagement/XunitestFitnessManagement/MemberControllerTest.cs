using FitnessBL.Models;
using FitnessManagement.API.Controllers;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
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
            // Mock repositories
            mockMemberRepo = new Mock<IMemberRepository>();
            mockReservationRepo = new Mock<IReservationRepository>();

            // Maak services aan met gemockte repositories
            memberService = new MemberService(mockMemberRepo.Object);
            reservationService = new ReservationService(mockReservationRepo.Object);

            // Maak de controller met de echte services
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
            mockMemberRepo.Setup(repo => repo.GetMember(2))
                .Returns(new Member(2, "Carl", "De Vught", "carl.devught@google.com", "Adegem", new DateOnly(1960, 1, 12), null, Member.MemberType.Silver, null));

            var result = memberController.Get(2);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectID_ReturnCorrectMember() {
            Member expectedMember = new Member(2, "Carl", "De Vught", "carl.devught@google.com", "Adegem", new DateOnly(1960, 1, 12), null, Member.MemberType.Silver, null);

            mockMemberRepo.Setup(repo => repo.GetMember(2)).Returns(expectedMember);

            var result = memberController.Get(2).Result as OkObjectResult;

            Assert.IsType<Member>(result.Value);
            Assert.Equal(2, (result.Value as Member).MemberId);
            Assert.Equal("Carl", (result.Value as Member).FirstName);
            Assert.Equal("De Vught", (result.Value as Member).LastName);
            Assert.Equal("carl.devught@google.com", (result.Value as Member).Email);
            Assert.Equal("Adegem", (result.Value as Member).Address);
            Assert.Equal(new DateOnly(1960, 1, 12), (result.Value as Member).Birthday);
            Assert.Equal(Member.MemberType.Silver, (result.Value as Member).Type);
        }
    }
}
