using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessBL.Models;
using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;

namespace FitnessManagement.BL.Models {
    public class CyclingSession : TrainingSessionBase{
       
        private DateTime _date;
        private int _duration;
        private int _avgWatt;
        private int _maxWatt;
        private int _avgCadence;
        private int _maxCadence;
        private string _comment;


        public CyclingSession() {
            TrainingSessionType = TrainingSessionType.Cycling;
        }

        public CyclingSession(int trainingId, DateTime date, int duration, int avgWatt, int maxWatt, int avgCadence, int maxCadence, string comment, Member cyclingMember, CyclingTrainingType type) {
            TrainingId = trainingId;
            _date = date;
            _duration = duration;
            _avgWatt = avgWatt;
            _maxWatt = maxWatt;
            _avgCadence = avgCadence;
            _maxCadence = maxCadence;
            _comment = comment;
            Member = cyclingMember;
            Type = type;
            TrainingSessionType = TrainingSessionType.Cycling;
           
        }

        public override int TrainingId { get; set; }
        public override Member Member { get; set; }

        
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

        public override int Duration {
            get => _duration;
            set {
                if (value <= 0) {
                    throw new CyclingSessionException("Duration must be greater than 0.");
                }
                _duration = value;
            }
        }

        public override DateTime Date {
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
            recovery,
            NoType
        }
        
        public override string GetImpact() {
            if (AvgWatt < 150 && Duration <= 90) return "low";
            if (AvgWatt < 150 && Duration > 90) return "medium";
            if (AvgWatt >= 150 && AvgWatt <= 200) return "medium";
            if (AvgWatt > 200) return "high";
            return "unknown";
        }


        public override string ToString() {
            return $"CyclingSession ID: {TrainingId}, Member ID: {Member}, Date: {Date.ToShortDateString()}, " +
                   $"Duration: {Duration} mins, AvgWatt: {AvgWatt}, MaxCadence: {MaxCadence}, Type: {Type}, Comment: {Comment}";
        }

        
    }
}
