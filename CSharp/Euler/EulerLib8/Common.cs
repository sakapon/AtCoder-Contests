using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace EulerLib8
{
	public static class Common
	{
		public static string GetText(string url)
		{
			using var http = new HttpClient();
			var task = http.GetStringAsync(url);
			task.Wait();
			return task.Result;
		}
	}
}
