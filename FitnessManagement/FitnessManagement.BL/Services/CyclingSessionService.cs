using FitnessManagement.BL.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Services {
    public class CyclingSessionService {
        private ICyclingRepository repo;

        public CyclingSessionService(ICyclingRepository repo) {
            this.repo = repo;
        }


    }
}
