using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FitnessManagement.BL.Intefaces {
    public interface IRunningSessionRepository {
        
        RunningSession GetById(int id);
       
        List<RunningSession> SessionsForMember(int memberId);
        List<RunningSession> GetByCustomerAndDate(int memberId, int year,int month);
      
      
    }

}
