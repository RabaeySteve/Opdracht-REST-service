using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClientWPF.Model {
    public class Member {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateOnly Birthday { get; set; }
        public List<string>? Interests { get; set; }
        public String Type { get; set; }

        public override string? ToString() {
            return $"{MemberId}, {FirstName} {LastName} {Email}";
        }
    }
}
