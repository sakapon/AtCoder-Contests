using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var n = int.Parse(Console.ReadLine());

		if (IsPrime(n))
		{
			Console.WriteLine("impossible");
		}
		else if (n % 2 == 0)
		{
			var m = n / 2;
			Console.WriteLine(m);
			for (int i = 0; i < m; i++)
				Console.WriteLine($"2 {2 * i + 1} {2 * n - 2 * i - 1}");
		}
		else
		{
			var m = (int)Factorize(n)[0];
			var sticks = Enumerable.Range(0, n).Select(i => 2 * i + 1).ToArray();
			var rm = Enumerable.Range(0, m).ToArray();
			var rm_r = rm.Reverse().ToArray();

			// n/m 本ずつ使います。
			var r = Array.ConvertAll(new int[m], _ => new List<int>());

			// 2*m 本を除いて、長いほうから使い方が容易に決まります (snake)。
			var ris = Enumerable.Range(0, n / m - 2).SelectMany(i => i % 2 == 0 ? rm : rm_r);
			foreach (var (l, i) in sticks.Skip(2 * m).Reverse().Zip(ris))
				r[i].Add(l);

			// 2 本ずつ使って、長さが 2 だけ異なる棒を m 本作ります。
			// 3m+1, ..., 4m, ..., 5m-1
			var shorts = sticks.Take(m).ToArray();
			var sums = rm.Select(i => 3 * m + 1 + 2 * i).ToArray();
			var sums_map = rm.ToDictionary(i => sums[i]);

			for (int i = 0, s = 4 * m; s >= sums[0]; i++, s -= 2)
			{
				r[sums_map[s]].Add(shorts[i]);
				r[sums_map[s]].Add(s - shorts[i]);
			}
			for (int i = m - 1, s = 4 * m + 2; s <= sums.Last(); i--, s += 2)
			{
				r[sums_map[s]].Add(shorts[i]);
				r[sums_map[s]].Add(s - shorts[i]);
			}

			Console.WriteLine(m);
			for (int i = 0; i < m; i++)
				Console.WriteLine($"{n / m} {string.Join(" ", r[i])}");
		}
		Console.Out.Flush();
	}

	static bool IsPrime(long n)
	{
		for (long x = 2; x * x <= n; ++x) if (n % x == 0) return false;
		return n > 1;
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
