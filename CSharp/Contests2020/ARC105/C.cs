using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (T, T) ToTuple2<T>(T[] a) => (a[0], a[1]);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var w = Read();
		(int l, int v)[] lvs = Array.ConvertAll(new bool[m], _ => ToTuple2(Read()));

		if (lvs.Min(lv => lv.v) < w.Max()) { Console.WriteLine(-1); return; }

		lvs = lvs.OrderBy(lv => lv.v).ToArray();
		var lmaxs = lvs.Select(lv => lv.l).ToArray().CumMax(0);

		int GetAllLength(int[] w)
		{
			var dp = new int[n];
			var wsum = new CumSum(w);

			for (int i = 0; i < n; i++)
				for (int j = i + 1; j < n; j++)
				{
					var li = Last(-1, m - 1, x => lvs[x].v < wsum.Sum(i, j + 1));
					dp[j] = Math.Max(dp[j], dp[i] + lmaxs[li + 1]);
				}

			return dp.Last();
		}

		var r = 1 << 30;
		Permutation(w, n, p => r = Math.Min(r, GetAllLength(p)));
		Console.WriteLine(r);
	}

	static void Permutation<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];
		var u = new bool[n];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				if (u[j]) continue;
				p[i] = values[j];
				u[j] = true;

				if (i2 < r) Dfs(i2);
				else action(p);

				u[j] = false;
			}
		}
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}

class CumSum
{
	long[] s;
	public CumSum(int[] a)
	{
		s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
	}
	public long Sum(int l_in, int r_ex) => s[r_ex] - s[l_in];
}

static class Seq
{
	public static TR[] Cumulate<TS, TR>(this TS[] a, TR r0, Func<TR, TS, TR> func)
	{
		var r = new TR[a.Length + 1];
		r[0] = r0;
		for (int i = 0; i < a.Length; ++i) r[i + 1] = func(r[i], a[i]);
		return r;
	}
	public static int[] CumMax(this int[] a, int v0 = int.MinValue) => Cumulate(a, v0, Math.Max);
	public static int[] CumMin(this int[] a, int v0 = int.MaxValue) => Cumulate(a, v0, Math.Min);
}
