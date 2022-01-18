using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Policy;

namespace ValuteConverter
{
	public class RateData
	{
		private ExchangeRate rate;
		private Dictionary<string, Valute> sortedDict = new Dictionary<string, Valute>();

		public Dictionary<string, Valute> SortedDict
		{
			get { return sortedDict; }
		}

		public bool LoadRateData()
		{
			string jsonData = "";

			try
			{
				WebClient client = new WebClient();

				using (Stream stream = client.OpenRead("https://www.cbr-xml-daily.ru/daily_json.js"))
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						string line = "";

						while ((line = reader.ReadLine()) != null)
							jsonData += line;
					}
				}

				rate = JsonConvert.DeserializeObject<ExchangeRate>(jsonData);
				
				Valute rub = new Valute();
				rub.Name = "Российский Рубль";
				rub.CharCode = "RUB";
				rub.Nominal = 1.0M;
				rub.Value = 1.0M;
				rate.Valute.Add("RUB", rub);

				foreach (var pair in rate.Valute.OrderBy(pair => pair.Value.Name))
				{
					sortedDict.Add(pair.Key, pair.Value);
				}
			}
			catch (WebException) { return true; }

			return false;
		}

		public DateTime GetDateOfRate() => rate.Date;

		public Valute GetValute(string key) => rate.Valute[key];

		public int GetIndex(string key)
		{
			var keys = rate.Valute.Keys;
			int index = 0;

			foreach (string i in keys)
			{
				if (i == key) return index;
				else index++;
			}

			return index;
		}

		public Dictionary<string, Valute> GetValuteList() => rate.Valute;

		public decimal ConvertValute(decimal value, string fromKey, string toKey)
		{
			Valute fromValute = GetValute(fromKey);
			Valute toValute = GetValute(toKey);

			return (value * fromValute.Value * toValute.Nominal) / (fromValute.Nominal * toValute.Value); 
		}
	}
}
