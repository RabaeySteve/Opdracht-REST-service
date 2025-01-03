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

        public MemberRepositoryEF(string connectionString) {
            this.ctx = new FitnessManagementContext(connectionString);
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

               return MapMember.MapToDomain(ctx.members.Where(m => m.MemberId == id).AsNoTracking().FirstOrDefault(), ctx);
                
               
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - GetMember", ex);
            }
        }

        public List<Member> GetMembers() {
            try {
                
                List<MemberEF> memberEFs = ctx.members.AsNoTracking().ToList();

                return memberEFs.Select(x => MapMember.MapToDomain(x, ctx)).ToList();
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
        public bool IsMember(string firstname, string adress, DateOnly birthday) {
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
                return ctx.members.Any(m => m.MemberPrograms.Any(p => p.ProgramCode == programCode));
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - IsProgram", ex);
            }
        }
        public bool ProgramMemberExist(string programCode, int memberId) {
            try {
                MemberEF memberEF = ctx.members.Include(m => m.MemberPrograms).FirstOrDefault(m => m.MemberId == memberId);
                ProgramEF programEF = ctx.program.FirstOrDefault(p => p.ProgramCode == programCode);

                return memberEF.MemberPrograms.Any(mp => mp.ProgramCode == programCode);
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - ProgramMemberExist", ex);
            }
        }
        public void AddProgram(int memberId, string programCode) {
            try {
                MemberEF memberEF = ctx.members.Include(m => m.MemberPrograms).FirstOrDefault(m => m.MemberId == memberId);
                ProgramEF programEF = ctx.program.FirstOrDefault(p => p.ProgramCode == programCode);

                bool exists = memberEF.MemberPrograms.Any(mp => mp.ProgramCode == programCode);

                int currentMembers= ctx.programmembers.Where(p => p.ProgramCode == programCode).Count();

                
                if (!exists && currentMembers < programEF.MaxMembers) {
                    ProgramMember programMember = new ProgramMember {
                        ProgramCode = programCode,
                        MemberId = memberId,
                        Program = programEF,
                        Member = memberEF
                    };
                    ctx.programmembers.Add(programMember);
                    
                }
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - AddProgram", ex);
            }
        }

        public int GetAllProgramMembers(string programCode) {
            try {
                List<MemberEF> memberEFs = ctx.members
                .Include(x => x.MemberPrograms)
                .Where(x => x.MemberPrograms.Any(mp => mp.ProgramCode == programCode))
                .ToList();
                return memberEFs.Count();
            } catch (Exception ex) {

                throw new RepoException("MemberRepo - GetAllProgramMembers", ex);
            }
        }
    }
}
