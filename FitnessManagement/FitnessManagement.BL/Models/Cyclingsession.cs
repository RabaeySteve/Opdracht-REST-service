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
        private int _maxWatt;
        private int _avgCadence;
        private int _maxCadence;
        private string _comment;


        public CyclingSession() {
        }

        public CyclingSession(int cyclingSessionId, DateTime date, int duration, int avgWatt, int maxWatt, int avgCadence, int maxCadence, string comment, CyclingTrainingType type, Member cyclingMember) {
            CyclingSessionId = cyclingSessionId;
            _date = date;
            _duration = duration;
            _avgWatt = avgWatt;
            _maxWatt = maxWatt;
            _avgCadence = avgCadence;
            _maxCadence = maxCadence;
            _comment = comment;
            Type = type;
            CyclingMember = cyclingMember;
           
        }

        public int CyclingSessionId { get; set; }
        public Member CyclingMember { get; set; }

        
        public string? Comment {
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
            set { if (value < 0) {
                    throw new CyclingSessionException("AvgCadence must be higher then 0");
                }
               
                _avgCadence = value; }
        }

        public int MaxCadence {
            get => _maxCadence;
            set {
                if (value < 0) {
                    throw new CyclingSessionException("MaxCadence must be higher then 0");
                }
                if (value < AvgCadence) {
                    throw new CyclingSessionException("MaxCadence cannot be lower than AvgCadence");
                }
                _maxCadence = value;
            }
        }

        public int AvgWatt {
            get => _avgWatt;
            set {
                if (value < 0) {
                    throw new CyclingSessionException("AvgWatt must be higher then 0");
                }
                _avgWatt = value;
            }
        }
        public int MaxWatt {
            get => _maxWatt;
            set {
                if (value < 0) {
                    throw new CyclingSessionException("MaxWatt must be 0 or higher.");
                }
                if (value < AvgWatt) {
                    throw new CyclingSessionException("MaxWatt cannot be lower than AvgWatt.");
                }
                _maxWatt = value;
            }
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
