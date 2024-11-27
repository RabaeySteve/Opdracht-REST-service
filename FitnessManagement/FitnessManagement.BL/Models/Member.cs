using FitnessManagement.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Models {
    public enum MemberType {
        Bronze,
        Silver,
        Gold
    }

    public class Member {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _address;
        private DateTime _birthday;
        private List<string> _interests = new List<string>();
        private MemberType _memberType;

        public Member() { }

        public Member(string firstName, string lastName, string email, string address, DateTime birthday, List<string> interests, MemberType memberType, int memberId) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Birthday = birthday;
            Interests = interests ?? new List<string>(); // Gebruik een lege lijst als fallback
            MemberType = memberType;
            MemberId = memberId;
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

        public string Email {
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

        public List<string> Interests {
            get => _interests;
            set {
                if (value == null)
                    throw new MemberException("Interests cannot be null.");
                _interests = value;
            }
        }

        public MemberType MemberType {
            get => _memberType;
            set => _memberType = value; // Geen extra validatie nodig omdat het een enum is
        }

        public override string ToString() {
            return $"Member: {FirstName} {LastName}, Email: {Email}, birthday: {Birthday}, Membertype: {MemberType}";
        }


    }
}

