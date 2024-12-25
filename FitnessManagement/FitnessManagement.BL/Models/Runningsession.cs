using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
namespace FitnessManagement.BL.Models {
    public class RunningSession : TrainingSessionBase {
        private DateTime _date;
        private int _duration;
        private double _avgSpeed;

        public RunningSession() {
            TrainingSessionType = TrainingSessionType.Running;
        }

        public RunningSession(int runningSessionId, Member member, DateTime date, int duration, double avgSpeed, List<RunningSessionDetail> details) {
            TrainingId = runningSessionId;
            Member = member;
            _date = date;
            _duration = duration;
            _avgSpeed = avgSpeed;
            Details = details;
            TrainingSessionType = TrainingSessionType.Running;
        }

        public List<RunningSessionDetail> Details { get; set; } = new List<RunningSessionDetail>();

        public override int TrainingId { get; set; }
        public override Member Member { get; set; }

        public override DateTime Date {
            get => _date;
            set {
                if (value.Date > DateTime.Now.Date) {
                    throw new RunningSessionException("Date cannot be in the future.");
                }
                _date = value;
            }
        }

        public override int Duration {
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
       
        public override string GetImpact() {
            return "Unknown";
        }

        public override string ToString() {
            return $"RunningSession ID: {TrainingId}, Date: {Date.ToShortDateString()}, Member ID: {Member.MemberId}, Duration: {Duration} mins, Avg Speed: {AvgSpeed} km/h";
        }
    }
}
