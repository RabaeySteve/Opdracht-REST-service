using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Models {
    public class Program {
		private string _name;
		
		private DateTime _startDate;
		private int _maxMembers;

		public int MaxMembers {
			get { return _maxMembers; }
			set { _maxMembers = value; }
		}

		public DateTime StartDate {
			get { return _startDate; }
			set { _startDate = value; }
		}

		public string Target { get; set; }

		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public string ProgramCode { get; set; }
    }
}
