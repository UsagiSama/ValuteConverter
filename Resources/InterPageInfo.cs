using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuteConverter
{
	class InterPageInfo
	{
		public bool LeftOrRight { get; set; }
		public RateData Rates { get; set; }
		public string LeftValute { get; set; }
		public string RightValute { get; set; }
		//public decimal LeftValue { get; set; }
		//public decimal RightValue { get; set; }
	}
}
