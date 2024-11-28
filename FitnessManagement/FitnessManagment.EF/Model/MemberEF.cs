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

        public MemberEF(int memberId, string firstName, string lastName, string address, string? email, DateTime birthday, string? interests, string? memberType) {
            MemberId = memberId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            Birthday = birthday;
            Interests = interests;
            MemberType = memberType;
            
        }

        [Key]
        public int MemberId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(45)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(45)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Email { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? Interests { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? MemberType { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

    }
}
