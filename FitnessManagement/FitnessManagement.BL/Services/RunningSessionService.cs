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

        public bool IsRunningSession(int id) {
            try {
                return repo.IsRunningSession(id);
            } catch (Exception) {
                throw;
            }
        }

        public RunningSession GetById(int id) {
            try {
                if (repo.IsRunningSession(id)) throw new RunningSessionException("Running session does not exist.");
                return repo.GetById(id);
            } catch (Exception) {
                throw;
            }
        }

        public RunningSession AddSession(RunningSession session) {
            try {
                if (repo.IsRunningSession(session.RunningSessionId)) throw new RunningSessionException("Running session already exists.");

                int runninSessionId = repo.GetAll().Count() +1;
                session.RunningSessionId = runninSessionId;

                foreach (RunningSessionDetail detail in session.Details) {
                    detail.RunningSessionId = runninSessionId;

                }


                repo.AddSession(session);
                return session;
            } catch (Exception) {
                throw;
            }
        }

        public void Delete(int id) {
            try {
                if (!repo.IsRunningSession(id)) throw new RunningSessionException("Running session does not exist.");
                repo.Delete(id);
            } catch (Exception) {
                throw;
            }
        }

        public RunningSession UpdateSession(RunningSession session) {
            try {
                if (!repo.IsRunningSession(session.RunningSessionId)) throw new RunningSessionException("Running session does not exist.");
                repo.UpdateSession(session);
                return session;
            } catch (Exception) {
                throw;
            }
        }

        public List<RunningSession> SessionsForMember(int memberId) {
            try {
                return repo.SessionsForMember(memberId);
            } catch (Exception) {
                throw;
            }
        }
    }
}
