using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());
		var sum = d.Values.Sum(c => c * (c - 1) / 2);

		// 各グループ内の個数
		var rn = (int)Math.Sqrt(n);
		var gc = (n - 1) / rn + 1;
		var gs = Enumerable.Range(0, gc).Select(i => new Group(a[(i * rn)..Math.Min(n, (i + 1) * rn)])).ToArray();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (l0, r, x) in qs)
		{
			var l = l0 - 1;

			var delta = new Dictionary<int, int>();
			for (int gl = l / rn * rn; gl < r; gl += rn)
			{
				var li = Math.Max(gl, l);
				var ri = Math.Min(gl + rn, r);
				delta = CollectionHelper.Merge(delta, gs[gl / rn].Set(li - gl, ri - gl, x));
			}

			foreach (var (k, dc) in delta)
			{
				if (dc == 0) continue;

				var c = d.GetValue(k, 0);
				sum -= c * (c - 1) / 2;
				c += dc;
				sum += c * (c - 1) / 2;

				d[k] = c;
			}

			Console.WriteLine(sum);
		}
		Console.Out.Flush();
	}

	class Group
	{
		int n;
		int[] a;
		int x_all;

		public bool AreAllSame => x_all > 0;

		public Group(int[] a)
		{
			n = a.Length;
			this.a = a;

			if (a.Distinct().Count() == 1)
			{
				x_all = a[0];
			}
		}

		public Dictionary<int, int> Set(int l, int r, int x)
		{
			var lr = r - l;
			var d = new Dictionary<int, int> { [x] = lr };

			if (AreAllSame)
			{
				d[x_all] = -lr;

				if (lr == n)
				{
					x_all = x;
				}
				else
				{
					Array.Fill(a, x_all);
					Array.Fill(a, x, l, lr);
					x_all = 0;
				}
			}
			else
			{
				for (int i = l; i < r; i++)
				{
					var v = a[i];
					if (d.ContainsKey(v)) d[v]--;
					else d[v] = -1;
				}

				if (lr == n)
				{
					x_all = x;
				}
				else
				{
					Array.Fill(a, x, l, lr);
				}
			}
			return d;
		}
	}
}

static class CollectionHelper
{
	public static TV GetValue<TK, TV>(this Dictionary<TK, TV> d, TK k, TV v0 = default) => d.ContainsKey(k) ? d[k] : v0;

	// 項目数が大きいほうにマージします。
	public static Dictionary<TK, int> Merge<TK>(Dictionary<TK, int> d1, Dictionary<TK, int> d2)
	{
		if (d1.Count < d2.Count) (d1, d2) = (d2, d1);
		foreach (var (k, v) in d2)
			if (d1.ContainsKey(k)) d1[k] += v;
			else d1[k] = v;
		return d1;
	}
}
