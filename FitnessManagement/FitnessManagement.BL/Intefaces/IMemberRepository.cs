using FitnessBL.Models;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface IMemberRepository {
        IEnumerable<Member> GetMembers();
        Member GetMember(int id);
        bool IsMember(int id);
        Member AddMember(Member member);
        Member UpdateMember(Member member);

        void DeleteMember(int id);


        IEnumerable<Reservation> GetReservation(int memberId);
        IEnumerable<Program> GetProgram(int memberId);
        IEnumerable<Cyclingsession> GetCyclingSessionsForMember(int memberId);
        IEnumerable<Runningsession> GetRunningSessionsForMember(int memberId);

        
    }
}
