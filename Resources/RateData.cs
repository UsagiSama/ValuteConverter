using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Policy;

namespace ValuteConverter
{
	public class RateData
	{
		private ExchangeRate rate;

		public string LoadRateData()
		{
			string jsonData = LoadRateDataAsync().Result;

			if (jsonData != "Ошибка подключения")
			{
				rate = JsonConvert.DeserializeObject<ExchangeRate>(jsonData);
				return "Успешно";
			}
			else return jsonData;			
		}

		private async Task<string> LoadRateDataAsync()
		{
			string jsonData = "";

			try
			{
				HttpWebRequest request = WebRequest.Create("https://www.cbr-xml-daily.ru/daily_json.js") as HttpWebRequest;
				HttpWebResponse response = await request.GetResponseAsync().ConfigureAwait(false) as HttpWebResponse;

				using (Stream stream = response.GetResponseStream()) 
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						string line = "";

						while ((line = reader.ReadLine()) != null)
							jsonData += line;
					}
				}
			}
			catch (WebException) { return "Ошибка подключения"; } 

			return jsonData;
		}

		/*public string LoadRateData()
		{
			try
			{
				string jsonData = "";
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
			}
			catch (WebException) { return "Ошибка подключения"; }

			return "Успешно";
		}*/

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

			return value * toValute.Nominal * (fromValute.Value / toValute.Value);
		}
	}
}
