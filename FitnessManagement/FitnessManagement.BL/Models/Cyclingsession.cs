using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;

namespace FitnessManagement.BL.Models {
    public class CyclingSession {
       
        private DateTime _date;
        private int _duration;
        private int _avgWatt;
        private int _avgCadence;
        private int _maxCadence;
        private int _maxWatt;


        public CyclingSession() {
        }

        public CyclingSession(DateTime date, int duration, int avgWatt, int maxCadence, int avgCadence, int maxWatt, int cyclingSessionId, Member member, string comment, CyclingTrainingType type) {
            Date = date; 
            Duration = duration; 
            AvgWatt = avgWatt; 
            MaxCadence = maxCadence; 
            MaxWatt = maxWatt;
            AvgCadence = avgCadence;
            CyclingSessionId = cyclingSessionId;
            CyclingMember = member;
            Comment = comment;
            Type = type;
        }

        

        public int CyclingSessionId { get; set; }
        public Member CyclingMember { get; set; }

        private string _comment;
        public string Comment {
            get => _comment;
            set {
                if (value?.Length > 500) {
                    throw new CyclingSessionException("Comment cannot exceed 500 characters.");
                }
                _comment = value;
            }
        }

        public CyclingTrainingType Type { get; set; }
        public int AvgCadence {
            get { return _avgCadence; }
            set { _avgCadence = value; }
        }

        public int MaxCadence {
            get => _maxCadence;
            set {
                if (value < 0) {
                    throw new CyclingSessionException("MaxCadence must be a positive value.");
                }
                _maxCadence = value;
            }
        }

        public int AvgWatt {
            get => _avgWatt;
            set {
                if (value < 0) {
                    throw new CyclingSessionException("AvgWatt must be a positive value.");
                }
                _avgWatt = value;
            }
        }
        public int MaxWatt {
            get { return _maxWatt; }
            set { _maxWatt = value; }
        }



        public int Duration {
            get => _duration;
            set {
                if (value <= 0) {
                    throw new CyclingSessionException("Duration must be greater than 0.");
                }
                _duration = value;
            }
        }

        public DateTime Date {
            get => _date;
            set {
                if (value.Date > DateTime.Now.Date) {
                    throw new CyclingSessionException("Date cannot be in the future.");
                }
                _date = value;
            }
        }

        public enum CyclingTrainingType {
            fun,
            endurance,
            interval,
            ecovery,
            NoType
        }
        public override string ToString() {
            return $"CyclingSession ID: {CyclingSessionId}, Member ID: {CyclingMember}, Date: {Date.ToShortDateString()}, " +
                   $"Duration: {Duration} mins, AvgWatt: {AvgWatt}, MaxCadence: {MaxCadence}, Type: {Type}, Comment: {Comment}";
        }
    }
}
