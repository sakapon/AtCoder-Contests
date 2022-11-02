﻿using System;
using System.Collections.Generic;
using System.Linq;

class P051
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int digits = 6;
		const int count = 8;
		var format = $"D{digits}";
		var n = (int)Math.Pow(10, digits);

		var b = GetPrimes(n);

		for (int x = 1; x < n; x += 2)
		{
			var s = x.ToString(format);
			if (!s.Contains('0')) continue;
			s = string.Join("", s.Select(c => c == '0' ? '1' : '0'));
			var d = int.Parse(s);

			var c = 0;
			for (int i = s[0] == '0' ? 0 : 1; i < 10; i++)
			{
				if (!b[x + d * i])
				{
					c++;
				}
			}
			if (c >= count) return s[0] == '0' ? x : x + d;
		}
		return -1;
	}

	static bool[] GetPrimes(int n)
	{
		var b = new bool[n + 1];
		for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
		return b;
	}
}
