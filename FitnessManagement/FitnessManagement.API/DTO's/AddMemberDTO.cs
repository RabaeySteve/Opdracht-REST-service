using FitnessManagement.BL.Exceptions;
using static FitnessBL.Models.Member;
using System.Net;

namespace FitnessManagement.API.DTO_s {
    public class AddMemberDTO {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName {get; set; }
        public string? Email {get; set; }
        public string Address {get; set; }
        public DateTime Birthday {get ; set; }
        public List<string>? Interests {get; set; }
        public String? Type { get; set; }
    }
}
