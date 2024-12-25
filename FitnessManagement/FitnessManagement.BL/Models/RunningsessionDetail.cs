using FitnessManagement.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Models {
    public class RunningSessionDetail {
        private int _intervalTime;
        private double _intervalSpeed;

        public RunningSessionDetail() {
        }

        public RunningSessionDetail(int runningSessionId, int seqNr, int intervalTime, double intervalSpeed) {
            RunningSessionId = runningSessionId;
            SeqNr = seqNr;
            IntervalTime = intervalTime;
            IntervalSpeed = intervalSpeed;
        }

        public int RunningSessionId { get; set; }
        public int SeqNr { get; set; } 

        public int IntervalTime {
            get => _intervalTime;
            set {
                if (value <= 0) {
                    throw new RunningSessionException("Interval time must be greater than 0.");
                }
                _intervalTime = value;
            }
        }

        public double IntervalSpeed {
            get => _intervalSpeed;
            set {
                if (value <= 0) {
                    throw new RunningSessionException("Interval speed must be greater than 0.");
                }
                _intervalSpeed = value;
            }
        }

        

        public override string? ToString() {
            return $"RunningSession ID: {RunningSessionId}, Seq Nr: {SeqNr}, Interval Time: {IntervalTime} sec, Interval Speed: {IntervalSpeed} km/h";
        }
    }
}
