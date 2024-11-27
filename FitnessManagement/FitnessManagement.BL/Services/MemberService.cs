using FitnessBL.Models;
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


        public MemberService(IMemberRepository repo) {
            this.repo = repo;
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
        public Member AddMember(Member member) {
            try {
                if (IsMember(member.MemberId)) {
                    throw new MemberException("Member bestaat al");
                } else {
                    
                    return repo.AddMember(member);
                }
            } catch (Exception ex) {

                throw new MemberException("AddMember", ex); 
            }
        }
        public Member UpdateMember(Member member) {
            try {
                if (IsMember(member.MemberId)) {
                    return repo.UpdateMember(member);
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
                }  } catch (Exception ex) {

                throw new MemberException("DeleteMember", ex);
            
            }
        }
        public IEnumerable<Reservation> GetReservation(int memberId) {
            try {
                return repo.GetReservation(memberId);
            } catch (Exception ex) {

                throw new MemberException("GetReservation", ex);
            }
        }
        public IEnumerable<Program> GetProgram(int memberId) {
            try {
                return repo.GetProgram(memberId);
            } catch (Exception ex) {

                throw new MemberException("GetProgram", ex);
            }
        }
        public IEnumerable<Cyclingsession> GetCyclingSessionsForMember(int memberId) {
            try {
                return repo.GetCyclingSessionsForMember(memberId);
            } catch (Exception ex) {

                throw new MemberException("GetCyclingSessionsForMember", ex);
            }
        }
        public IEnumerable<Runningsession> GetRunningSessionsForMember(int memberId) {
            try {
                return repo.GetRunningSessionsForMember(memberId);
            } catch (Exception ex) {

                throw new MemberException("GetRunningSessionsForMember", ex);
            }
        }
    }
}
