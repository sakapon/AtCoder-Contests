using System;
using System.Linq;

class N3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], q = h[1];
		var rs = new int[n].Select(_ => Read()).ToArray();
		var ps = new int[q].Select(_ => Read()).ToArray();

		var ys = rs.Select(r => r[1]).Concat(rs.Select(r => r[1] + r[2])).Concat(ps.Select(p => p[1])).Distinct().OrderBy(v => v).ToArray();
		var yd = Enumerable.Range(0, ys.Length).ToDictionary(i => ys[i]);

		// ソートのため、cost を -1 倍。
		var xqs = rs.Select(r => (x: r[0], c: -r[3], ymin: yd[r[1]], ymax: yd[r[1] + r[2]] + 1))
			.Concat(rs.Select(r => (x: r[0] + r[2], c: r[3], ymin: yd[r[1]], ymax: yd[r[1] + r[2]] + 1)))
			.Concat(ps.Select((p, id) => (x: p[0], c: 0, ymin: yd[p[1]], ymax: id)))
			.OrderBy(xq => xq);

		var cost = new long[q];
		var st = new STR<long>(ys.Length, (x, y) => x + y, 0);

		foreach (var (_, c, ymin, ymax) in xqs)
			if (c == 0)
				cost[ymax] = st.Get(ymin);
			else
				st.Set(ymin, ymax, -c);

		Console.WriteLine(string.Join("\n", cost));
	}
}
