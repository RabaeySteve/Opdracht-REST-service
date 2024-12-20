using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.DTO_s.DTOModels {
    public class TrainingSession {
      public int Id { get; set; }
      public int MemberId { get; set; }
      public DateOnly Date { get; set; }
      public int Duration { get; set; } 
      public string TrainingType { get; set; } 
        
    }

    

}
