using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessManagement.BL.Exceptions;
namespace FitnessManagement.BL.Models {
    public class RusnningSession {
        private DateTime _date;
        private int _duration;
        private float _avgSpeed;

        public List<RusnningSessionDetail> Details { get; set; } = new List<RusnningSessionDetail>();

        public int RusnningSessionId { get; set; }
        public int MemberId { get; set; }

        public DateTime Date {
            get => _date;
            set {
                if (value.Date > DateTime.Now.Date) {
                    throw new RusnningSessionException("Date cannot be in the future.");
                }
                _date = value;
            }
        }

        public int Duration {
            get => _duration;
            set {
                if (value <= 0) {
                    throw new RusnningSessionException("Duration must be greater than 0.");
                }
                _duration = value;
            }
        }

        public float AvgSpeed {
            get => _avgSpeed;
            set {
                if (value <= 0) {
                    throw new RusnningSessionException("Average speed must be greater than 0.");
                }
                _avgSpeed = value;
            }
        }

        public override string ToString() {
            return $"RusnningSession ID: {RusnningSessionId}, Date: {Date.ToShortDateString()}, Member ID: {MemberId}, Duration: {Duration} mins, Avg Speed: {AvgSpeed} km/h";
        }
    }
}
