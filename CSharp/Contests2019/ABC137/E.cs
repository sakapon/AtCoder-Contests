using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		map = Enumerable.Range(0, h[1]).Select(i => read()).Select(r => new R { A = r[0], B = r[1], C = r[2] - h[2] }).GroupBy(r => r.A).ToDictionary(g => g.Key, g => g.ToArray());

		cs = Enumerable.Repeat(long.MinValue, h[0] + 1).ToArray();
		cs[1] = 0;
		FindCircuit(1);
		Console.WriteLine(cs[h[0]] == long.MaxValue ? -1 : Math.Max(cs[h[0]], 0));
	}

	static Dictionary<int, R[]> map;
	static HashSet<int> ps = new HashSet<int>();
	static Stack<R> rs = new Stack<R>();
	static long[] cs;

	static void FindCircuit(int p)
	{
		if (!map.ContainsKey(p)) return;

		if (!ps.Add(p))
		{
			var loop = rs.Reverse().SkipWhile(r => r.A != p).ToArray();
			if (loop.Sum(r => r.C) > 0) foreach (var r in loop) cs[r.A] = long.MaxValue;
			return;
		}

		foreach (var r in map[p])
		{
			if (cs[p] == long.MaxValue) cs[r.B] = cs[p];
			else
			{
				var c = cs[p] + r.C;
				if (cs[r.B] < c) cs[r.B] = c;
			}

			rs.Push(r);
			FindCircuit(r.B);
			rs.Pop();
		}
		ps.Remove(p);
	}
}

struct R
{
	public int A, B, C;
}
