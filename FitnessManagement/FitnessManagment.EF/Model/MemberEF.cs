using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    
    public class MemberEF {
        public MemberEF() { }

        public MemberEF(string firstName, string lastName, string address, string? email, string? interests, string? type, DateOnly birthday) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Interests = interests;
            Type = type;
            Birthday = birthday;
        }

        public MemberEF(int memberId, string firstName, string lastName, string? email, string address, DateOnly birthday, string? interests, string? memberType, List<ProgramMember> memberPrograms) {
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
        [Column("member_id")]
        public int MemberId { get; set; }

        [Required]
        [Column("first_name", TypeName = "nvarchar(45)")]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name", TypeName = "nvarchar(45)")]
        public string LastName { get; set; }

        [Column("email", TypeName = "nvarchar(50)")]
        public string? Email { get; set; }

        [Required]
        [Column("address", TypeName = "nvarchar(200)")]
        public string Address { get; set; }

        [Required]
        [Column("birthday", TypeName = "date")]
        public DateOnly Birthday { get; set; }

        [Column("interests", TypeName = "nvarchar(500)")]
        public string? Interests { get; set; }

        [Column("membertype", TypeName = "nvarchar(20)")]
        public string? Type { get; set; }
        
        public List<ProgramMember> MemberPrograms { get; set; } = new List<ProgramMember>();
    }
}
