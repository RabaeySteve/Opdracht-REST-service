using FitnessManagement.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Models {
    public class RunningsessionDetail {
        private int _intervalTime;
        private float _intervalSpeed;

        public RunningsessionDetail(int runningsessionId, int seqNr, int intervalTime, float intervalSpeed) {
            RunningsessionId = runningsessionId;
            SeqNr = seqNr;
            IntervalTime = intervalTime;
            IntervalSpeed = intervalSpeed;
        }

        public int RunningsessionId { get; set; }
        public int SeqNr { get; set; } 

        public int IntervalTime {
            get => _intervalTime;
            set {
                if (value <= 0) {
                    throw new RunningsessionException("Interval time must be greater than 0.");
                }
                _intervalTime = value;
            }
        }

        public float IntervalSpeed {
            get => _intervalSpeed;
            set {
                if (value <= 0) {
                    throw new RunningsessionException("Interval speed must be greater than 0.");
                }
                _intervalSpeed = value;
            }
        }

        

        public override string? ToString() {
            return $"Runningsession ID: {RunningsessionId}, Seq Nr: {SeqNr}, Interval Time: {IntervalTime} sec, Interval Speed: {IntervalSpeed} km/h";
        }
    }
}
