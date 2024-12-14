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
        private float _avgWatt;
        private float _avgCadence;
        private float _maxCadence;
        private float _maxWatt;

       


        public CyclingSession(DateTime date, int duration, float avgWatt, float maxCadence, float avgCadence, float maxWatt, int cyclingSessionId, Member member, string comment, CyclingTrainingType type) {
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
        public float AvgCadence {
            get { return _avgCadence; }
            set { _avgCadence = value; }
        }

        public float MaxCadence {
            get => _maxCadence;
            set {
                if (value < 0) {
                    throw new CyclingSessionException("MaxCadence must be a positive value.");
                }
                _maxCadence = value;
            }
        }

        public float AvgWatt {
            get => _avgWatt;
            set {
                if (value < 0) {
                    throw new CyclingSessionException("AvgWatt must be a positive value.");
                }
                _avgWatt = value;
            }
        }
        public float MaxWatt {
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
            Fun,
            Endurance,
            Interval,
            Recovery,
            NoType
        }
        public override string ToString() {
            return $"CyclingSession ID: {CyclingSessionId}, Member ID: {MemberId}, Date: {Date.ToShortDateString()}, " +
                   $"Duration: {Duration} mins, AvgWatt: {AvgWatt}, MaxCadence: {MaxCadence}, Type: {Type}, Comment: {Comment}";
        }
    }
}
