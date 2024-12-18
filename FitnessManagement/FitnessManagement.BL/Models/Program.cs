using FitnessManagement.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Models {
    public class Program {
		private DateTime _startDate;
        private int _maxMembers;
		private string _name;
		private string _programCode;

        public Program() {
        }

        public Program(string programCode, string name, DateTime startDate, ProgramTarget target, int maxMembers) {
            _programCode = programCode;
            _name = name;
            _startDate = startDate;
            Target = target;
            _maxMembers = maxMembers;
        }

        public string ProgramCode {
			get { return _programCode; }
			set {
                if (value.Length > 10) {
                    throw new ProgramException("ProgramCode can't be longer then 10 characters");
                }

                _programCode = value; }
		}

        public ProgramTarget Target { get; set; }
        public string Name {
			get { return _name; }
			set {if (string.IsNullOrEmpty(value)) {
					throw new ProgramException("Must declare a name!");
				}if (value.Length > 40) {
                    throw new ProgramException("Program name can't be longer then 40 characters");
                }
				
				_name = value; }
		}
		public DateTime StartDate {
			get { return _startDate; }
			set {if (value.Date < DateTime.Now.Date) {
				throw new ProgramException("Date can't be in the past");
				}
				
				_startDate = value; }
		}
		public int MaxMembers {
			get { return _maxMembers; }
			set {
				if (value <= 0) {
					throw new ProgramException("Members must be higher then 0");
				}
				
				_maxMembers = value; }
		}
        public enum ProgramTarget {
			
            beginner,
            advanced,
            pro,
            noType
        }
        public override string? ToString() {
			return $"ProgramCode: {ProgramCode}, Name : {Name}, Target: {Target}, Startdate: {StartDate.ToShortDateString()}, MaxMembers: {MaxMembers}";
        }
    }
}
