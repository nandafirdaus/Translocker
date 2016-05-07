using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Translocker
{
	public class RestUtils
	{
		HttpClient client;

		public RestUtils ()
		{
			client = new HttpClient ();
//			client.MaxResponseContentBufferSize = 256000;
		}

		public async Task<string> GetDataAsync (string url)
		{
			var uri = new Uri (string.Format (url, string.Empty));

			var response = await client.GetAsync (uri);
			if (response.IsSuccessStatusCode) {
				var content = await response.Content.ReadAsStringAsync ();

				return content;
			}

			return "";
		}

		public List<Busway> deserializeBuswayJson (string input)
		{
			return JsonConvert.DeserializeObject <List<Busway>> (input);
		}
	}
}

