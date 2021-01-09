using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (T, T, T) ToTuple3<T>(T[] a) => (a[0], a[1], a[2]);
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var abc = ToTuple3(h.Skip(1).ToArray());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var dp = Array.ConvertAll(new bool[n + 1], _ => new Dictionary<(int a, int b, int c), (char c, (int, int, int) pv)>());
		dp[0][abc] = default;

		for (int i = 0; i < n; i++)
		{
			foreach (var (t, _) in dp[i])
			{
				var (a, b, c) = t;
				switch (s[i])
				{
					case "AB":
						if (a <= b && b > 0) dp[i + 1][(a + 1, b - 1, c)] = ('A', t);
						if (a >= b && a > 0) dp[i + 1][(a - 1, b + 1, c)] = ('B', t);
						break;
					case "BC":
						if (c <= b && b > 0) dp[i + 1][(a, b - 1, c + 1)] = ('C', t);
						if (c >= b && c > 0) dp[i + 1][(a, b + 1, c - 1)] = ('B', t);
						break;
					case "AC":
						if (a <= c && c > 0) dp[i + 1][(a + 1, b, c - 1)] = ('A', t);
						if (a >= c && a > 0) dp[i + 1][(a - 1, b, c + 1)] = ('C', t);
						break;
					default:
						break;
				}
			}
			if (dp[i + 1].Count == 0) { Console.WriteLine("No"); return; }
		}

		var r = new Stack<char>();
		var v = dp[n].Values.First();
		for (int i = n - 1; i >= 0; i--)
		{
			r.Push(v.c);
			v = dp[i][v.pv];
		}
		Console.WriteLine("Yes");
		Console.WriteLine(string.Join("\n", r));
	}
}
