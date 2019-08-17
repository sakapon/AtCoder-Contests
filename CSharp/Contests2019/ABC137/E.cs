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

		// 無限増加する閉区間の起点を検出します。
		FindCircuit(1);
		// TODO: その頂点から終点に到達できるかを判定します。
		if (holes.Any()) { Console.WriteLine(-1); return; }

		var d = new Dictionary<int, long> { [1] = 0 };
		for (var i = 0; i < h[0]; i++)
			foreach (var p in d.Keys.Where(p => map.ContainsKey(p)).ToArray())
				foreach (var r in map[p])
				{
					var c = d[p] + r.C;
					if (!d.ContainsKey(r.B) || d[r.B] < c) d[r.B] = c;
				}
		Console.WriteLine(Math.Max(d[h[0]], 0));
	}

	static Dictionary<int, R[]> map;
	static HashSet<int> holes = new HashSet<int>();
	static HashSet<int> ps = new HashSet<int>();
	static Stack<R> rs = new Stack<R>();

	static void FindCircuit(int p)
	{
		if (!map.ContainsKey(p)) return;

		if (!ps.Add(p))
		{
			if (rs.Reverse().SkipWhile(r => r.A != p).Sum(r => r.C) > 0) holes.Add(p);
			return;
		}

		foreach (var r in map[p])
		{
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
