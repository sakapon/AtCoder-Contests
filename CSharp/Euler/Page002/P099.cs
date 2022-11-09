﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

class P099
{
	const string textUrl = "https://projecteuler.net/project/resources/p099_base_exp.txt";
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var t = GetText(textUrl).Split('\n')
			.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
			.Select((a, i) => (i, v: a[1] * Math.Log(a[0])))
			.Aggregate((x, y) => Max(x, y, p => p.v));
		return t.i + 1;
	}

	public static T Max<T>(T o1, T o2, Func<T, double> toKey) => toKey(o1) >= toKey(o2) ? o1 : o2;

	public static string GetText(string url)
	{
		using var http = new HttpClient();
		var task = http.GetStringAsync(url);
		task.Wait();
		return task.Result;
	}
}