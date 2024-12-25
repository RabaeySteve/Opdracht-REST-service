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
        private DateOnly _birthday;
        private List<string> _interests = new List<string>();


        public Member() { }

        public Member(int memberId, string firstName, string lastName, string email, string address, DateOnly birthday, List<string> interests, MemberType memberType, List<Program> programs) {
            MemberId = memberId;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _address = address;
            _birthday = birthday;
            _interests = interests;
            Type = memberType;
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

        public string Email {
            get => _email;
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new MemberException("Email cannot be empty or null.");
                }

                if (value.Length > 50) {
                    throw new MemberException("Email cannot exceed 50 characters.");
                }

                if (!IsValidEmail(value)) {
                    throw new MemberException("Invalid email format.");
                }

                _email = value;
            }
        }

        public string Address {
            get => _address;
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new MemberException("Address cannot be empty.");
                }
                if (value.Length > 200) {
                    throw new MemberException("Address cannot exceed 200 characters.");
                }
                _address = value;
            }
        }

        public DateOnly Birthday {
            get => _birthday;
            set {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);

                if (value > today)
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
        public MemberType Type { get; set; }

        public List<Program> Programs { get; set; } = new List<Program>();
        public enum MemberType {
            noType,
            Bronze,
            Silver,
            Gold
        }

        public override string ToString() {
            string programstring = string.Join(",\n", Programs);
            return $"Member: {FirstName} {LastName}, Email: {Email}, birthday: {Birthday}, Membertype: {Type}, \n{programstring}";
        }
        private bool IsValidEmail(string email) {
           
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex);
        }


    }
}

