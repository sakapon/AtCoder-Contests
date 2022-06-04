using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var s = Read2();
		var g = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var ps_xy = ps.GroupBy(p => p.x).ToDictionary(g => g.Key, g => ToBSArray(g.Select(p => p.y).ToArray()));
		var ps_yx = ps.GroupBy(p => p.y).ToDictionary(g => g.Key, g => ToBSArray(g.Select(p => p.x).ToArray()));

		static BSArray<int> ToBSArray(int[] a)
		{
			Array.Sort(a);
			return new BSArray<int>(a);
		}

		var id_p = new List<(int, int)>();
		var p_id = new Dictionary<(int, int), int>();
		AddPoint(s);
		AddPoint(g);

		int AddPoint((int x, int y) p)
		{
			if (!p_id.ContainsKey(p))
			{
				p_id[p] = id_p.Count;
				id_p.Add(p);
			}
			return p_id[p];
		}

		var r = Bfs(4 * n, id =>
		{
			var (px, py) = id_p[id];

			var nids = new List<int>();

			if (ps_xy.ContainsKey(px))
			{
				{ if (ps_xy[px].GetLast(v => v < py).TryGetValue(out var y)) nids.Add(AddPoint((px, y + 1))); }
				{ if (ps_xy[px].GetFirst(v => v > py).TryGetValue(out var y)) nids.Add(AddPoint((px, y - 1))); }
			}
			if (ps_yx.ContainsKey(py))
			{
				{ if (ps_yx[py].GetLast(v => v < px).TryGetValue(out var x)) nids.Add(AddPoint((x + 1, py))); }
				{ if (ps_yx[py].GetFirst(v => v > px).TryGetValue(out var x)) nids.Add(AddPoint((x - 1, py))); }
			}

			return nids.ToArray();
		},
		0, 1);

		if (r[1] == long.MaxValue) return -1;
		return r[1];
	}

	public static long[] Bfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var nv in nexts(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				if (nv == ev) return costs;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}

public class BSArray<T>
{
	T[] a;
	public T[] Raw => a;
	public int Count => a.Length;
	public T this[int i] => a[i];
	public BSArray(T[] array) { a = array; }

	public int GetFirstIndex(Func<T, bool> predicate) => First(0, Count, i => predicate(a[i]));
	public int GetLastIndex(Func<T, bool> predicate) => Last(-1, Count - 1, i => predicate(a[i]));

	public Maybe<T> GetFirst(Func<T, bool> predicate)
	{
		var i = GetFirstIndex(predicate);
		return i != Count ? a[i] : Maybe<T>.None;
	}
	public Maybe<T> GetLast(Func<T, bool> predicate)
	{
		var i = GetLastIndex(predicate);
		return i != -1 ? a[i] : Maybe<T>.None;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}

public struct Maybe<T>
{
	public static readonly Maybe<T> None = new Maybe<T>();

	T v;
	public bool HasValue { get; }
	public Maybe(T value)
	{
		v = value;
		HasValue = true;
	}

	public static explicit operator T(Maybe<T> value) => value.Value;
	public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);

	public T Value => HasValue ? v : throw new InvalidOperationException("This has no value.");
	public T GetValueOrDefault(T defaultValue = default(T)) => HasValue ? v : defaultValue;
	public bool TryGetValue(out T value)
	{
		value = HasValue ? v : default(T);
		return HasValue;
	}
}
