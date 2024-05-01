using System;
using System.Collections.Generic;
using System.Linq;
using GRL005.DataTrees;

class CSP
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		map = Array.ConvertAll(new bool[n], _ => Read().Skip(1).ToArray());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		tour = new List<(int, int)>();
		first = new int[n];
		DFS(0, 0);

		var monoid = new Monoid<(int, int)>((x, y) => x.Item1 <= y.Item1 ? x : y, (int.MaxValue, -1));
		var st = new SparseTable<(int, int)>(tour.ToArray(), monoid);

		return string.Join("\n", qs.Select(q =>
		{
			var l = first[q[0]];
			var r = first[q[1]];
			return l <= r ? st[l, r + 1].Item2 : st[r, l + 1].Item2;
		}));
	}

	static int[][] map;
	static List<(int, int)> tour;
	static int[] first;

	static void DFS(int v, int depth)
	{
		first[v] = tour.Count;
		tour.Add((depth, v));
		foreach (var nv in map[v])
		{
			DFS(nv, depth + 1);
			tour.Add((depth, v));
		}
	}
}

namespace GRL005.DataTrees
{
	public static class Monoid
	{
		public static Monoid<int> Int32_Add { get; } = new Monoid<int>((x, y) => x + y);
		public static Monoid<long> Int64_Add { get; } = new Monoid<long>((x, y) => x + y);
		public static Monoid<int> Int32_Min { get; } = new Monoid<int>((x, y) => x <= y ? x : y, int.MaxValue);
		public static Monoid<int> Int32_Max { get; } = new Monoid<int>((x, y) => x >= y ? x : y, int.MinValue);

		public static Monoid<int> Int32_ArgMin(int[] a) => new Monoid<int>((x, y) => a[x] <= a[y] ? x : y);

		public static Monoid<int> Int32_Update { get; } = new Monoid<int>((x, y) => x != -1 ? x : y, -1);
	}

	public struct Monoid<T>
	{
		public Func<T, T, T> Op;
		public T Id;
		public Monoid(Func<T, T, T> op, T id = default(T)) { Op = op; Id = id; }
	}

	public class SparseTable<TValue>
	{
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		readonly int n;
		readonly List<TValue[]> values;

		public SparseTable(TValue[] a, Monoid<TValue> monoid)
		{
			(op, iv) = (monoid.Op, monoid.Id);
			n = a.Length;
			values = new List<TValue[]> { a };

			var p = 1;
			while (p < n)
			{
				var t = values[values.Count - 1];
				var b = new TValue[n];
				for (int i = 0; i < n; i++)
					b[i] = i + p < n ? op(t[i], t[i + p]) : t[i];
				values.Add(b);
				p <<= 1;
			}
		}

		public TValue this[int l, int r] => Get(l, r);
		public TValue Get(int l, int r)
		{
			if (l < 0) l = 0;
			if (r > n) r = n;

			var c = r - l;
			if (c == 0) return iv;
			if (c == 1) return values[0][l];

			var k = 0;
			while (true)
			{
				if ((1 << ++k) >= c)
				{
					--k;
					return op(values[k][l], values[k][r - (1 << k)]);
				}
			}
		}
	}
}
