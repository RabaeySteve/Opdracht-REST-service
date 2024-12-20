using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
namespace FitnessManagement.BL.Models {
    public class RunningSession : ITrainingSession {
        private DateTime _date;
        private int _duration;
        private double _avgSpeed;

        public RunningSession() {
        }

        public RunningSession(int runningSessionId, Member member, DateTime date, int duration, double avgSpeed, List<RunningSessionDetail> details) {
            TrainingId = runningSessionId;
            RunningMember = member;
            _date = date;
            _duration = duration;
            _avgSpeed = avgSpeed;
            Details = details;
        }

        public List<RunningSessionDetail> Details { get; set; } = new List<RunningSessionDetail>();

        public int TrainingId { get; set; }
        public Member RunningMember { get; set; }

        public DateTime Date {
            get => _date;
            set {
                if (value.Date > DateTime.Now.Date) {
                    throw new RunningSessionException("Date cannot be in the future.");
                }
                _date = value;
            }
        }

        public int Duration {
            get => _duration;
            set {
                if (value <= 0) {
                    throw new RunningSessionException("Duration must be greater than 0.");
                }
                _duration = value;
            }
        }

        public double AvgSpeed {
            get => _avgSpeed;
            set {
                if (value <= 0) {
                    throw new RunningSessionException("Average speed must be greater than 0.");
                }
                _avgSpeed = value;
            }
        }
        public string TrainingType { get; set; }
        public string GetImpact() {
            return "Unknown";
        }

        public override string ToString() {
            return $"RusnningSession ID: {TrainingId}, Date: {Date.ToShortDateString()}, Member ID: {RunningMember.MemberId}, Duration: {Duration} mins, Avg Speed: {AvgSpeed} km/h";
        }
    }
}
