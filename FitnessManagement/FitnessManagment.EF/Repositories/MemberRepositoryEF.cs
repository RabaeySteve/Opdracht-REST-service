﻿using FitnessBL.Models;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF.Exceptions;
using FitnessManagement.EF.Mappers;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Repositories {
    public class MemberRepositoryEF : IMemberRepository {
        private FitnessManagementContext ctx;

        public MemberRepositoryEF(string connectioString) {
            this.ctx = new FitnessManagementContext(connectioString);
        }
        private void SaveAndClear() {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public void AddMember(Member member) {
            try {
                ctx.members.Add(MapMember.MapToDB(member));
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - AddMember", ex);
            }
        }

        public void DeleteMember(int id) {
            try {
                MemberEF member = ctx.members.Where(m => m.MemberId == id).FirstOrDefault();
                if (member != null) {
                    ctx.members.Remove(member);
                    SaveAndClear();
                }

            } catch (Exception ex) {

                throw new RepoException("MemberRepo - DeleteMember", ex);
            }
        }



        public Member GetMember(int id) {
            try {

                return MapMember.MapToDomain(ctx.members.Where(m => m.MemberId == id).AsNoTracking().FirstOrDefault());
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - GetMember", ex);
            }
        }

        public IEnumerable<Member> GetMembers() {
            try {
                return ctx.members.Select(x => MapMember.MapToDomain(x)).ToList();
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - GetMembers", ex);
            }
        }



        public bool IsMember(int id) {
            try {
                return ctx.members.Any(x => x.MemberId == id);
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - IsMember", ex);
            }
        }
        public bool IsMember(string firstname, string adress, DateTime birthday) {
            try {
                return ctx.members.Any(x => x.FirstName == firstname && x.Address == adress && x.Birthday == birthday);
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - IsMember", ex);
            }
        }

        public void UpdateMember(Member member) {
            try {
                ctx.members.Update(MapMember.MapToDB(member));
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - UpdateMember", ex);
            }
        }


        public bool IsProgram(string programCode) {
            try {
                return
            } catch (Exception) {

                throw;
            }
        }
        public void AddProgram(int memberId, string programCode) {
            throw new NotImplementedException();
        }

        public void DeleteProgram(int memberId, string programCode) {
            throw new NotImplementedException();
        }

        public List<Program> GetProgramsByMemberId(Dictionary<int, Program> programs, int memberId) {
            throw new NotImplementedException();
        }
    }
}
