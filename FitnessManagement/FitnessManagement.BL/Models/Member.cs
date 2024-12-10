using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Models {
   

    public class Member {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _address;
        private DateTime _birthday;
        private List<string> _interests = new List<string>();
        

        public Member() { }

        public Member(int memberId, string firstName, string lastName, string? email, string address, DateTime birthday, List<string>? interests, MemberType? memberType, Dictionary<int, Program> programs) {
            MemberId = memberId;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _address = address;
            _birthday = birthday;
            _interests = interests ?? new List<string>(); // Gebruik een lege lijst als fallback
            Type = memberType;
            Programs = programs;
        }

        public Member(string firstName, string lastName, string? email, string address, DateTime birthday, List<string>? interests, MemberType? type, Dictionary<int, Program> programs) {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _address = address;
            _birthday = birthday;
            _interests = interests;
            Type = type;
            Programs = programs;
        }



        public int MemberId { get; set; }

        public string FirstName {
            get => _firstName;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new MemberException("First name cannot be empty.");
                if (value.Length > 20)
                    throw new MemberException("First name cannot be longer than 20 characters.");
                _firstName = value;
            }
        }

        public string LastName {
            get => _lastName;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new MemberException("Last name cannot be empty.");
                if (value.Length > 20)
                    throw new MemberException("Last name cannot be longer than 20 characters.");
                _lastName = value;
            }
        }

        public string? Email {
            get => _email;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new MemberException("Email cannot be empty.");
                _email = value;
            }
        }

        public string Address {
            get => _address;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new MemberException("Address cannot be empty.");
                _address = value;
            }
        }

        public DateTime Birthday {
            get => _birthday;
            set {
                if (value > DateTime.Now)
                    throw new MemberException("Birthday cannot be in the future.");
                _birthday = value;
            }
        }

        public List<string>? Interests {
            get => _interests;
            set {
                if (value == null)
                    throw new MemberException("Interests cannot be null.");
                _interests = value;
            }
        }
        public MemberType? Type { get; set; }

        public Dictionary<int, Program> Programs { get; set; } = new Dictionary<int, Program>();
        public enum MemberType {
            noType,
            Bronze,
            Silver,
            Gold
        }

        public override string ToString() {
            return $"Member: {FirstName} {LastName}, Email: {Email}, birthday: {Birthday}, Membertype: {Type}";
        }


    }
}

