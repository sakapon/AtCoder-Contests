using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var n = h[0];
		var rs = new int[h[1]].Select(_ => read()).ToArray();
		var map = rs.GroupBy(x => x[0]).ToDictionary(g => g.Key, g => g.Select(x => x[1]).ToArray());

		var u = new bool[n + 1];
		var t = new bool[n + 1];
		var q = new Stack<int>();
		var qi = new int[n + 1];
		var ps = rs.Select(x => x[1]).Distinct().ToArray();
		foreach (var sp in ps)
		{
			if (u[sp]) continue;
			u[sp] = t[sp] = true;
			q.Push(sp);
			while (q.Any())
			{
				var p = q.Pop();
				if (!map.ContainsKey(p)) continue;
				foreach (var np in map[p])
				{
					if (u[np])
					{
						if (t[np])
						{
							var closed = true;
							var l = new List<int> { p };
							var xp = p;
							while (xp != np)
							{
								if (qi[xp] == 0) { closed = false; break; }
								l.Add(xp = qi[xp]);
							}
							if (!closed) continue;

							var cs = new HashSet<int>(l);
							if (rs.Count(x => cs.Contains(x[0]) && cs.Contains(x[1])) == cs.Count)
							{
								Console.WriteLine(cs.Count);
								Console.WriteLine(string.Join("\n", cs));
								return;
							}
							else continue;
						}
						else
						{
							t[np] = true;
							continue;
						}
					}

					u[np] = t[np] = true;
					qi[np] = p;
					q.Push(np);
				}
			}
			Array.Clear(t, 0, n + 1);
		}
		Console.WriteLine(-1);
	}
}
