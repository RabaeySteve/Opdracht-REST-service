﻿using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Services {
    public class MemberService {
        private IMemberRepository repo;
        //private IProgramRepository programRepo;

        public MemberService(IMemberRepository repo/*, IProgramRepository programRepo*/) {
            this.repo = repo;
            //this.programRepo = programRepo;
        }

        public IEnumerable<Member> GetMembers() {
            try {
                return repo.GetMembers();
            } catch (Exception ex) {

                throw new MemberException("GetMembers", ex);
            }
        }
        public Member GetMember(int id) {
            try {
                if (!repo.IsMember(id)) throw new MemberException("GetMember - Gebruiker bestaat niet");
                return repo.GetMember(id);
            } catch (Exception ex) {

                throw new MemberException("GetMember", ex);
            }
        }
        public bool IsMember(int id) {
            try {
                return repo.IsMember(id);
            } catch (Exception ex) {

                throw new MemberException("IsMember", ex);
            }
        }
        public bool IsMember(string firstname, string adress, DateOnly birthday) {
            try {
                return repo.IsMember(firstname, adress, birthday);
            } catch (Exception ex) {

                throw new MemberException("IsMember", ex);
            }
        }
        public Member AddMember(Member member) {
            try {
                if (IsMember(member.MemberId) && IsMember(member.FirstName, member.Address, member.Birthday)) {
                    throw new MemberException("Member bestaat al");
                } else {
                    repo.AddMember(member);
                    return member;
                }
            } catch (Exception ex) {

                throw new MemberException("AddMember", ex);
            }
        }
        public Member UpdateMember(Member member) {
            try {
                if (IsMember(member.MemberId)) {
                    repo.UpdateMember(member);
                    return member;
                } else {
                    throw new MemberException("Member bestaat niet");
                }
            } catch (Exception ex) {

                throw new MemberException("UpdateMember", ex);
            }
        }
        public void DeleteMember(int id) {
            try {
                if (IsMember(id)) {
                    repo.DeleteMember(id);
                } else {
                    throw new MemberException("Member bestaat niet");
                }
            } catch (Exception ex) {

                throw new MemberException("DeleteMember", ex);

            }
        }
        public bool IsProgram(string programCode) {
            try {
               return repo.IsProgram(programCode);
            } catch (Exception ex) {

                throw new MemberException("IsProgram", ex);
            }
        }
        public void AddProgram(int memberId, string programCode) {
            try {
                if (!IsProgram(programCode)) {
                    throw new MemberException("Program doesn't excist");
                }
                int aantalMembers = repo.GetAllProgramMembers(programCode);
                //Program program = programRepo.GetProgramByProgramCode(programCode);
                //if (program.MaxMembers == aantalMembers) { 
                //    throw new MemberException("Maximum members for this program");
                //}
                
               repo.AddProgram(memberId, programCode);
            } catch (Exception ex) {

                throw new MemberException("IsProgram", ex);
            }
        }
        int GetAllProgramMembers(string programCode) {
            try {
                if (!IsProgram(programCode)) {
                    throw new MemberException("");
                }
                return repo.GetAllProgramMembers(programCode);
            } catch (Exception ex) {

                throw new MemberException("GetAllProgramMembers", ex);
            }
        }
        public List<Program> GetProgramsByMemberId(Dictionary<int, Program> programs, int memberId) {
            try {
                return new List<Program> { programs[memberId] };
            } catch (Exception ex) {

                throw new MemberException("GetProgramsByMemberId", ex);
            }
        }
    }
}
