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

		var r = ss.Sum(Score);
		var comp = Comparer<(long x, long y)>.Create((v1, v2) => -(v1.x * v2.y).CompareTo(v2.x * v1.y));
		r += Score(ss.Select(Compress).OrderBy(p => p, comp));
		return r;
	}

	static (long, long) Compress(string s)
	{
		return (s.Count(c => c == 'X'), s.Where(c => c != 'X').Sum(c => (long)(c - '0')));
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
