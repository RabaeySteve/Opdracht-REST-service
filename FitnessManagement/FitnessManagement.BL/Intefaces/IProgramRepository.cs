using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface IProgramRepository {
        IEnumerable<Program> GetAll();
        
        Program GetProgramByProgramCode(string programCode);
        bool IsProgram(string programCode);
        void AddProgram(Program Program);
        void UpdateProgram(Program Program);
        bool IsProgramNew(string name, DateTime startDate);
    }
}
