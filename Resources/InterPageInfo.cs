using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuteConverter
{
	class InterPageInfo
	{
		public bool FromOrTo { get; set; }
		public RateData Rates { get; set; }
		public string CodeValute { get; set; }
	}
}
