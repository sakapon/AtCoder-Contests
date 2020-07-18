using System;
using System.Linq;

class N
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], q = h[1];
		var rs = new int[n].Select(_ => Read()).ToArray();
		var ps = new int[q].Select(_ => Read()).ToArray();

		var dy = rs.SelectMany(v => new[] { v[1], v[1] + v[2] })
			.Concat(ps.Select(v => v[1]))
			.Distinct()
			.OrderBy(y => y)
			.Select((y, i) => (y, i))
			.ToDictionary(v => v.y, v => v.i);

		var c = new long[q];
		var st = new ST(dy.Count);
		var qs = rs
			.Select(v => (x: v[0], X: v[0] + v[2], y: dy[v[1]], Y: dy[v[1] + v[2]], c: v[3]))
			.SelectMany(v => new[] { (q: -1, v.x, v.y, v.Y, v.c), (q: 1, x: v.X, v.y, v.Y, c: -v.c) })
			.Concat(ps.Select((v, id) => (q: 0, x: v[0], y: dy[v[1]], Y: 0, c: id)))
			.OrderBy(v => v.x)
			.ThenBy(v => v.q);

		foreach (var v in qs)
			if (v.q == 0)
				c[v.c] = st.Get(v.y);
			else
				st.Add(v.y, v.Y, v.c);
		Console.WriteLine(string.Join("\n", c));
	}
}

class ST
{
	int ln;
	long[][] vs;
	public ST(int n)
	{
		ln = (int)Math.Ceiling(Math.Log(n, 2));
		vs = Enumerable.Range(0, ln + 1).Select(i => new long[1 << ln - i]).ToArray();
	}

	public void Add(int m, int M, long v)
	{
		for (int k = 0, f = 1; k <= ln; k++, f *= 2)
		{
			if (m + f > M + 1) break;
			if ((m & f) != 0)
			{
				vs[k][m / f] += v;
				m += f;
			}
		}
		for (int k = ln, f = 1 << ln; k >= 0; k--, f /= 2)
		{
			if (M - m + 1 >= f)
			{
				vs[k][m / f] += v;
				m += f;
				if (m > M) break;
			}
		}
	}

	public long Get(int i)
	{
		var sum = 0L;
		for (int k = 0; k <= ln; k++, i /= 2) sum += vs[k][i];
		return sum;
	}
}
