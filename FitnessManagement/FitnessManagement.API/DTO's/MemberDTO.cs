using FitnessManagement.BL.Exceptions;
using static FitnessBL.Models.Member;
using System.Net;
using FitnessManagement.BL.Models;
using static FitnessManagement.BL.Models.Equipment;

namespace FitnessManagement.API.DTO_s {
    public class MemberDTO {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName {get; set; }
        public string? Email {get; set; }
        public string Address {get; set; }
        public DateTime Birthday {get ; set; }
        public List<string>? Interests {get; set; }
        public String? Type { get; set; }
    }
    public class MemberAddProgramDTO {
        public int MemberId { get; set; }
        public string ProgramCode { get; set; }
    }
    public class MemberProgramsDTO {
        public int MemberId { get; set;}
        public List<FitnessManagement.BL.Models.Program> ProgramsList { get; set; }
    }

    public class MemberReservationsDTO {
        public int MemberId { get; set;}
        public List<MemberReservationsListDTO> ReservationsList { get; set;}
    }
    public class MemberReservationsListDTO {
        
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public int StartTime { get; set; }
        public EquipmentType equipmentType { get; set; }
    }

}
