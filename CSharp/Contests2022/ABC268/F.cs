using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var comp = Comparer<(long x, long y)>.Create((v1, v2) => (v2.x * v1.y).CompareTo(v1.x * v2.y));
		var ps = Array.ConvertAll(ss, Compress);
		Array.Sort(ps, comp);
		return ss.Sum(Score) + Score(ps);
	}

	static (long x, long y) Compress(string s)
	{
		long x = 0, y = 0;
		foreach (var c in s)
			if (c == 'X') x++;
			else y += c - '0';
		return (x, y);
	}

	static long Score(string s)
	{
		long r = 0, x = 0;
		foreach (var c in s)
			if (c == 'X') x++;
			else r += x * (c - '0');
		return r;
	}

	static long Score(IEnumerable<(long, long)> s)
	{
		long r = 0, x = 0;
		foreach (var (x0, y) in s)
		{
			r += x * y;
			x += x0;
		}
		return r;
	}
}
