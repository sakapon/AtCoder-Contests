using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var K = int.Parse(Console.ReadLine());
		var a = Read();
		var D = int.Parse(Console.ReadLine());
		var x = Read();

		var map = ToWeightedListMap(n + 1, es, true);

		var costs = Array.ConvertAll(map, _ => (d: int.MaxValue, c: 0));
		var q = new SortedSet<(int d, int c, int v)>();
		foreach (var sv in a)
		{
			costs[sv] = (0, 0);
			q.Add((0, 0, sv));
		}

		// j 日より後で、2^i 日後までの w の最大値
		var D2 = D << 1;
		var logD = (int)Math.Log2(D2) + 1;
		var maxw = NewArray2(logD + 1, D2 + 1, -1);
		Array.Copy(x, maxw[0], D);
		for (int i = 1; i <= logD; i++)
		{
			var c = 1 << i - 1;
			for (int j = 0; j + c <= D2; j++)
			{
				maxw[i][j] = Math.Max(maxw[i - 1][j], maxw[i - 1][j + c]);
			}
		}

		// d 日より後で w 以上の距離がある最小の日
		int FindNextDay(int d, int w)
		{
			for (int i = logD - 1; i >= 0; i--)
			{
				if (maxw[i][d] < w)
				{
					d += 1 << i;
					if (d > D) break;
				}
			}
			return d + 1;
		}

		while (q.Count > 0)
		{
			var (d, c, v) = q.Min;
			q.Remove((d, c, v));

			foreach (var e in map[v])
			{
				var nv = e[1];
				var (nc, nd) = e[2] <= -c ? (c + e[2], d) : FindNextCost();
				if (nd > D) continue;

				(int c, int d) FindNextCost()
				{
					var nd = FindNextDay(d, e[2]);
					if (nd > D) return (0, nd);
					return (-x[nd - 1] + e[2], nd);
				}

				if (costs[nv].CompareTo((nd, nc)) <= 0) continue;
				if (costs[nv] != (int.MaxValue, 0)) q.Remove((costs[nv].d, costs[nv].c, nv));
				q.Add((nd, nc, nv));
				costs[nv] = (nd, nc);
			}
		}

		return string.Join("\n", costs[1..].Select(p => p.d).Select(d => d == int.MaxValue ? -1 : d));
	}

	public static List<int[]>[] ToWeightedListMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (twoWay) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
