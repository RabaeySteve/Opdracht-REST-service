using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Model {
    public class MemberEF {
        public MemberEF() {
        }

        public MemberEF(string firstName, string lastName, string address, string? email, string? interests, string? type, DateTime birthday) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Interests = interests;
            Type = type;
            Birthday = birthday;
        }

        public MemberEF(int memberId, string firstName, string lastName, string? email,string address, DateTime birthday, string? interests, string? memberType, List<ProgramMember> memberPrograms) {
            MemberId = memberId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Birthday = birthday;
            Interests = interests;
            Type = memberType;
            MemberPrograms = memberPrograms;
        }

      

        [Key]
        public int MemberId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(45)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(45)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; }



        [Column(TypeName = "nvarchar(500)")]
        public string? Interests { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? Type { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public List<ProgramMember> MemberPrograms { get; set; } = new List<ProgramMember>();

    }
}
