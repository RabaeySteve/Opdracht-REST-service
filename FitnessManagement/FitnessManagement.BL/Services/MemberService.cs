using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;

namespace FitnessManagement.BL.Services {
    public class MemberService {
        private readonly IMemberRepository repo;

        public MemberService(IMemberRepository repo) {
            this.repo = repo;
        }

        public List<Member> GetMembers() {
            try {
                return repo.GetMembers();
            } catch (Exception ex) {
                throw new MemberException("GetMembers - Unexpected error occurred", ex);
            }
        }

        public Member GetMember(int id) {
            try {
                if (!repo.IsMember(id)) {
                    throw new MemberException("GetMember - Member does not exist");
                }
                return repo.GetMember(id);
            } catch (Exception ex) {
                throw new MemberException("GetMember - Unexpected error occurred", ex);
            }
        }

        public bool IsMember(int id) {
            try {
                return repo.IsMember(id);
            } catch (Exception ex) {
                throw new MemberException("IsMember - Unexpected error occurred", ex);
            }
        }

        public bool IsMember(string firstname, string address, DateOnly birthday) {
            try {
                return repo.IsMember(firstname, address, birthday);
            } catch (Exception ex) {
                throw new MemberException("IsMember (with details) - Unexpected error occurred", ex);
            }
        }

        public Member AddMember(Member member) {
            try {
                if (IsMember(member.MemberId) || IsMember(member.FirstName, member.Address, member.Birthday)) {
                    throw new MemberException("AddMember - Member already exists");
                }

                repo.AddMember(member);
                return member;
            } catch (MemberException) {
                throw;
            } catch (Exception ex) {
                throw new MemberException("AddMember - Unexpected error occurred", ex);
            }
        }

        public Member UpdateMember(Member member) {
            try {
                if (!IsMember(member.MemberId)) {
                    throw new MemberException("UpdateMember - Member does not exist");
                }

                repo.UpdateMember(member);
                return member;
            } catch (MemberException) {
                throw;
            } catch (Exception ex) {
                throw new MemberException("UpdateMember - Unexpected error occurred", ex);
            }
        }

        public void DeleteMember(int id) {
            try {
                if (!IsMember(id)) {
                    throw new MemberException("DeleteMember - Member does not exist");
                }

                repo.DeleteMember(id);
            } catch (MemberException) {
                throw;
            } catch (Exception ex) {
                throw new MemberException("DeleteMember - Unexpected error occurred", ex);
            }
        }

        public bool IsProgram(string programCode) {
            try {
                return repo.IsProgram(programCode);
            } catch (Exception ex) {
                throw new MemberException("IsProgram - Unexpected error occurred", ex);
            }
        }

        public void AddProgram(int memberId, string programCode) {
            try {
                if (!IsProgram(programCode)) {
                    throw new MemberException("AddProgram - Program does not exist");
                }

                repo.AddProgram(memberId, programCode);
            } catch (MemberException) {
                throw;
            } catch (Exception ex) {
                throw new MemberException("AddProgram - Unexpected error occurred", ex);
            }
        }

        public int GetAllProgramMembers(string programCode) {
            try {
                if (!IsProgram(programCode)) {
                    throw new MemberException("GetAllProgramMembers - Program does not exist");
                }

                return repo.GetAllProgramMembers(programCode);
            } catch (MemberException) {
                throw;
            } catch (Exception ex) {
                throw new MemberException("GetAllProgramMembers - Unexpected error occurred", ex);
            }
        }

        public List<Program> GetProgramsByMemberId(Dictionary<int, Program> programs, int memberId) {
            try {
                if (!programs.ContainsKey(memberId)) {
                    throw new MemberException($"GetProgramsByMemberId - No programs found for member ID {memberId}");
                }

                return new List<Program> { programs[memberId] };
            } catch (Exception ex) {
                throw new MemberException("GetProgramsByMemberId - Unexpected error occurred", ex);
            }
        }
    }
}
