using System;
using System.Linq;

class H2
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var st = new ST_H(Read().Prepend(1 << 30).ToArray());
		var s = new int[int.Parse(Console.ReadLine())].Select(_ => Read()).ToArray();

		var r = 0L;
		foreach (var q in s)
		{
			if (q[0] == 1)
			{
				if (st.Get(q[1]) >= q[2])
				{
					st.Subtract(q[1], q[2]);
					r += q[2];
				}
			}
			else if (q[0] == 2)
			{
				if (st.Min(1) >= q[1])
				{
					st.SubtractAll(1, q[1]);
					r += (n + 1) / 2 * q[1];
				}
			}
			else
			{
				if (st.Min() >= q[1])
				{
					st.SubtractAll(q[1]);
					r += n * q[1];
				}
			}
		}
		Console.WriteLine(r);
	}
}

class ST_H
{
	long[][] vs;
	public ST_H(long[] c)
	{
		// raw, parity, all, min of raw for parity
		vs = new[] { c, new long[2], new long[1], "01".Select(p => c.Where((x, i) => i % 2 == p - '0').Min()).ToArray() };
	}

	public long Get(long i) => vs[0][i] + vs[1][i % 2] + vs[2][0];
	public long Min(int parity) => vs[1][parity] + vs[2][0] + vs[3][parity];
	public long Min() => Math.Min(Min(0), Min(1));

	public void Subtract(long i, long v) => vs[3][i % 2] = Math.Min(vs[3][i % 2], vs[0][i] -= v);
	public void SubtractAll(int parity, long v) => vs[1][parity] -= v;
	public void SubtractAll(long v) => vs[2][0] -= v;
}
