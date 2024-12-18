using FitnessBL.Models;
using FitnessManagement.API.Controllers;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XunitestFitnessManagement {
    public class MemberControllerTest {
        private readonly Mock<IMemberRepository> mockRepo;
        private readonly MemberController memberController;

        //public MemberControllerTest() {
        //    mockRepo=new Mock<IMemberRepository>();
        //    memberController = new MemberController(mockRepo.Object);
        //}

        //[Fact]
        //public void GET_UnknownID_ReturnNotFound() {
        //    mockRepo.Setup(repo => repo.GetMember(2))
        //         .Throws(new MemberException("Member does't exist."));

        //    var result = memberController.Get(2);            
        //    Assert.IsType<NotFoundObjectResult>(result.Result);
        //}
        //[Fact]
        //public void GET_CorrectID_ReturnOkResult() {
        //    mockRepo.Setup(repo => repo.GetMember(2))
        //         .Returns(new Member(2,"Carl", "De Vught", "carl.devught@google.com", "Adegem", new DateOnly(1960, 1, 12), null, Member.MemberType.Silver, null));

        //    var result = memberController.Get(2);
        //    Assert.IsType<OkObjectResult>(result.Result);
        //}
        //[Fact]
        //public void GET_CorrectID_ReturnCorrectMember() {
          
        //    Member expectedMember = new Member(2,"Carl","De Vught","carl.devught@google.com","Adegem",new DateOnly(1960, 1, 12),null,Member.MemberType.Silver,null);

           
        //    mockRepo.Setup(repo => repo.GetMember(2)).Returns(expectedMember);

           
        //    var result = memberController.Get(2).Result as OkObjectResult;
            
        //    Assert.IsType<Member>(result.Value);
        //    Assert.Equal(2, (result.Value as Member).MemberId);
        //    Assert.Equal("Carl", (result.Value as Member).FirstName);
        //    Assert.Equal("De Vught", (result.Value as Member).LastName);
        //    Assert.Equal("carl.devught@google.com", (result.Value as Member).Email);
        //    Assert.Equal("Adegem", (result.Value as Member).Address);
        //    Assert.Equal(new DateOnly(1960, 1, 12), (result.Value as Member).Birthday);
        //    Assert.Equal(Member.MemberType.Silver, (result.Value as Member).Type);
        //}

    }
}
