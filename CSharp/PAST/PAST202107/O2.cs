using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees.Bsts;

class O2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var abs = Array.ConvertAll(new bool[n], _ => Read2());

		var a = Array.ConvertAll(abs, p => p.a);
		var b = Array.ConvertAll(abs, p => p.b);
		var s = CumSumL(a);

		// B_i を購入するまでのコストの最小値
		var dp = new long[n + 1];

		// 購入前の所持金に対する最小のインデックス
		var set = new WBSet<long>();
		var map = new Dictionary<long, int>();
		const long max = 1L << 60;

		for (int i = 0; i < n; i++)
		{
			var nv = s[i + 1] - dp[i];
			if (set.Count == 0 || set.GetLast() < nv)
			{
				set.Add(nv);
				map[nv] = i;
			}

			if (i > 0 && b[i - 1] > b[i])
			{
				b[i] = b[i - 1];
			}

			var fv = set.GetFirst(x => x >= b[i], max);
			if (fv == max) return -1;
			var j = map[fv];
			dp[i + 1] = dp[j] + b[i];
		}

		return s[n] - dp[n];
	}

	public static long[] CumSumL(int[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public class WBSet<T>
	{
		public int Count { get; private set; }
		public T GetFirst() => throw new NotImplementedException();
		public T GetLast() => throw new NotImplementedException();
		public T GetFirst(Func<T, bool> predicate, T defaultValue = default(T)) => throw new NotImplementedException();
		public T GetLast(Func<T, bool> predicate, T defaultValue = default(T)) => throw new NotImplementedException();
		public bool Add(T item) => throw new NotImplementedException();
	}
}
