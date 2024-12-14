using static FitnessManagement.BL.Models.Program;

namespace FitnessManagement.API.DTO_s {
    public class ProgramDTO {
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public ProgramTarget Target { get; set; }
        public int MaxMembers { get; set; }


    }
}
