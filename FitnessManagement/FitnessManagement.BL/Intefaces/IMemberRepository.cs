using FitnessBL.Models;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface IMemberRepository {
        List<Member> GetMembers();
        Member GetMember(int id);
        bool IsMember(int id);
        bool IsMember(string firstname,string adress, DateOnly birthday);
        void AddMember(Member member);
        void UpdateMember(Member member);

        void DeleteMember(int id);
        bool IsProgram(string programCode);
        int GetAllProgramMembers(string programCode);
        void AddProgram(int memberId, string programCode);
       
    }
}
