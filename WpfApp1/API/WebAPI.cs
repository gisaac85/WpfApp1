using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.API
{
	public class WebAPI
	{
		static HttpClient client = new HttpClient();
		public static async Task<List<Post>> GetPosts(string path)
		{
			try
			{
				var model = new List<Post>();
				HttpResponseMessage response = await client.GetAsync(path);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					model = JsonConvert.DeserializeObject<List<Post>>(content);
				}
				return model;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
