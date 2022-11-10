using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

public static class Util
{
	public static string GetText(string url)
	{
		using var http = new HttpClient();
		var task = http.GetStringAsync(url);
		task.Wait();
		return task.Result;
	}
}
