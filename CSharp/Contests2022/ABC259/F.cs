using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static AsciiIO io = new AsciiIO();
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = io.Int();
		var d = io.Int(n);
		var es = io.Int(n - 1, 3);

		var map = ToMapList(n + 1, es, false);
		return Dfs(new[] { -1, 1, -1 }).Item1;

		// (親側の辺を選択しない場合の和, 選択した場合の変化量)
		(long, long) Dfs(int[] e)
		{
			var v = e[1];
			var c = e[2];
			var cap = d[v - 1];

			var s = 0L;
			var l = new List<long>();

			foreach (var ne in map[v])
			{
				if (ne[1] == e[0]) continue;

				var (ns, nd) = Dfs(ne);
				s += ns;
				if (nd > 0) l.Add(nd);
			}

			l.Sort();
			l.Reverse();

			s += l.Take(cap).Sum();
			return (s, cap == 0 ? 0 : c - (l.Count < cap ? 0 : l[cap - 1]));
		}
	}

	public static int[][][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	public static List<int[]>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}

public class AsciiIO
{
	static bool[] lf = new bool[1 << 7];
	static bool[] sp = new bool[1 << 7];
	static AsciiIO() => lf['\r'] = lf['\n'] = sp['\r'] = sp['\n'] = sp[' '] = true;

	System.IO.Stream si = new System.IO.BufferedStream(Console.OpenStandardInput(), 8192);

	int b, s;
	void NextValid() { while (sp[b = si.ReadByte()]) ; }
	bool Next() => !sp[b = si.ReadByte()];

	public int Int() => (int)Long();
	public int[] Int(int n) => Array(n, () => Int());
	public int[][] Int(int n, int m) => Array(n, () => Int(m));
	public (int, int) Int2() => (Int(), Int());
	public (int, int, int) Int3() => (Int(), Int(), Int());

	public long Long()
	{
		NextValid();
		if ((s = b) == '-') Next();
		var r = 0L;
		do r = r * 10 + (b & ~'0'); while (Next());
		return s == '-' ? -r : r;
	}

	public T[] Array<T>(int n, Func<T> f) { var r = new T[n]; for (var i = 0; i < n; ++i) r[i] = f(); return r; }
}
