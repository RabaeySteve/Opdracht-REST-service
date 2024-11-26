using FitnessBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Models {
    public class Member {
        
            
         private string _firstName;
         private string _lastName;
         private string _email;
         private string _address;
         private DateTime _birthday;
         private List<string> _interests;
         private string _memberType;

        public Member() {
          
        }

        public Member(string firstName, string lastName, string email, string address, DateTime birthday, List<string> interests, string memberType, int memberId) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Birthday = birthday;
            Interests = interests;
            MemberType = memberType;
            MemberId = memberId;
            
        }

        public int MemberId { get; set; }
             
            

            public string FirstName {
                get => _firstName;
                set {
                if (string.IsNullOrWhiteSpace(value))
                        throw new MemberException("First name cannot be empty.");
                if (value.Length > 20) {
                    throw new MemberException("First namr cannot be longer then 20 characters");
                }
                    _firstName = value;
                }
            }

            public string LastName {
                get => _lastName;
                set {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new MemberException("Last name cannot be empty.");
                if (value.Length > 20) {
                    throw new MemberException("Last name cannot be longer then 20 characters");
                }
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
                set => _interests =  new List<string>();
            }

            public string MemberType {
                get => _memberType;
                set {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new MemberException("Member type cannot be empty.");
                    if (value != "Bronze" && value != "Silver" && value != "Gold")
                        throw new MemberException("Member type must be Bronze, Silver, or Gold.");
                    _memberType = value;
                }
            }       
    }
}

