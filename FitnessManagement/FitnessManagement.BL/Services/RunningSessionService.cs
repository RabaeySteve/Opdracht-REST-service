using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;

namespace FitnessManagement.BL.Services {
    public class RunningSessionService {
        private IRunningSessionRepository repo;

        public RunningSessionService(IRunningSessionRepository repo) {
            this.repo = repo;
        }


        public List<RunningSession> SessionsForMember(int memberId) {
            try {
                return repo.SessionsForMember(memberId);
            } catch (Exception ex) {
                throw new RunningSessionException("SessionsForMember", ex);
            }
        }
        public List<RunningSession> GetByCustomerAndDate(int memberId, int year, int month) {
            try {
                return repo.GetByCustomerAndDate(memberId, year, month);
            } catch (Exception ex) {

                throw new RunningSessionException("GetByCustomerAndDate", ex);
            }
        }
    }
}
