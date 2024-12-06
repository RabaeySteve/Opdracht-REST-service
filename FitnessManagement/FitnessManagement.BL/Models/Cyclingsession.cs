using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessManagement.BL.Exceptions;

namespace FitnessManagement.BL.Models {
    public class Cyclingsession {
       
        private DateTime _date;
        private int _duration;
        private int _avgWatt;
        private int _maxCadence;

        
        public Cyclingsession(DateTime date, int duration, int avgWatt, int maxCadence, int cyclingSessionId, int memberId, string comment, CyclingTrainingType type) {
            Date = date; 
            Duration = duration; 
            AvgWatt = avgWatt; 
            MaxCadence = maxCadence; 
            CyclingSessionId = cyclingSessionId;
            MemberId = memberId;
            Comment = comment;
            Type = type;
        }

        
        public int CyclingSessionId { get; set; }
        public int MemberId { get; set; }

        private string _comment;
        public string Comment {
            get => _comment;
            set {
                if (value?.Length > 500) {
                    throw new CyclingsessionException("Comment cannot exceed 500 characters.");
                }
                _comment = value;
            }
        }

        public CyclingTrainingType Type { get; set; }

        public int MaxCadence {
            get => _maxCadence;
            set {
                if (value < 0) {
                    throw new CyclingsessionException("MaxCadence must be a positive value.");
                }
                _maxCadence = value;
            }
        }

        public int AvgWatt {
            get => _avgWatt;
            set {
                if (value < 0) {
                    throw new CyclingsessionException("AvgWatt must be a positive value.");
                }
                _avgWatt = value;
            }
        }

        public int Duration {
            get => _duration;
            set {
                if (value <= 0) {
                    throw new CyclingsessionException("Duration must be greater than 0.");
                }
                _duration = value;
            }
        }

        public DateTime Date {
            get => _date;
            set {
                if (value.Date > DateTime.Now.Date) {
                    throw new CyclingsessionException("Date cannot be in the future.");
                }
                _date = value;
            }
        }

        public enum CyclingTrainingType {
            Fun,
            Endurance,
            Interval,
            Recovery
        }
        public override string ToString() {
            return $"CyclingSession ID: {CyclingSessionId}, Member ID: {MemberId}, Date: {Date.ToShortDateString()}, " +
                   $"Duration: {Duration} mins, AvgWatt: {AvgWatt}, MaxCadence: {MaxCadence}, Type: {Type}, Comment: {Comment}";
        }
    }
}
