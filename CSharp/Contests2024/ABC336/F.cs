﻿class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Read());

		var sv = new ZobristGrid2<int>(h, w, s.SelectMany(x => x).ToArray());
		var ev = new ZobristGrid2<int>(h, w, Enumerable.Range(1, h * w).ToArray());

		var d1 = ShortestByBFS(GetNexts, sv, null, 10);
		var d2 = ShortestByBFS(GetNexts, ev, null, 10);

		var vs = d1.Keys.ToHashSet();
		vs.IntersectWith(d2.Keys);
		if (vs.Count == 0) return -1;
		return vs.Min(v => d1[v] + d2[v]);

		ZobristGrid2<int>[] GetNexts(ZobristGrid2<int> v)
		{
			var l = new List<ZobristGrid2<int>>();

			var nv = v.Clone();
			for (int i = 0; i < h - 1; ++i)
				for (int j = 0; j < w - 1; ++j)
					nv[i, j] = v[h - 2 - i, w - 2 - j];
			l.Add(nv);

			nv = v.Clone();
			for (int i = 0; i < h - 1; ++i)
				for (int j = 1; j < w; ++j)
					nv[i, j] = v[h - 2 - i, w - j];
			l.Add(nv);

			nv = v.Clone();
			for (int i = 1; i < h; ++i)
				for (int j = 0; j < w - 1; ++j)
					nv[i, j] = v[h - i, w - 2 - j];
			l.Add(nv);

			nv = v.Clone();
			for (int i = 1; i < h; ++i)
				for (int j = 1; j < w; ++j)
					nv[i, j] = v[h - i, w - j];
			l.Add(nv);

			return l.ToArray();
		}
	}

	public static Dictionary<T, long> ShortestByBFS<T>(Func<T, T[]> nexts, T sv, T ev, long maxCost = long.MaxValue)
	{
		var costs = new Dictionary<T, long>();
		var q = new Queue<T>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			if (costs.Comparer.Equals(v, ev)) return costs;
			var nc = costs[v] + 1;
			if (nc > maxCost) return costs;

			foreach (var nv in nexts(v))
			{
				if (costs.ContainsKey(nv)) continue;
				costs[nv] = nc;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}

public class ZobristGrid2<T> : IEquatable<ZobristGrid2<T>>
{
	// 下位ビットが分散するようにハッシュを生成します。
	static int CreateHash(int id, T value) => id * 1000003 + value.GetHashCode() * 10007;

	public readonly int n1, n2;
	public readonly T[] a;
	int hash;

	public ZobristGrid2(int _n1, int _n2, T[] _a, int _hash) => (n1, n2, a, hash) = (_n1, _n2, _a ?? new T[_n1 * _n2], _hash);
	public ZobristGrid2(int _n1, int _n2, T[] _a = null) : this(_n1, _n2, _a, 0)
	{
		for (int id = 0; id < a.Length; ++id)
			hash ^= CreateHash(id, a[id]);
	}

	public T this[int i, int j]
	{
		get => a[n2 * i + j];
		set
		{
			var id = n2 * i + j;
			hash ^= CreateHash(id, a[id]);
			hash ^= CreateHash(id, value);
			a[id] = value;
		}
	}

	public ZobristGrid2<T> Clone() => new ZobristGrid2<T>(n1, n2, (T[])a.Clone(), hash);

	#region Equality Operators
	public bool Equals(ZobristGrid2<T> other) => !(other is null) && Equals(a, other.a);
	public static bool Equals(ZobristGrid2<T> v1, ZobristGrid2<T> v2) => v1?.Equals(v2) ?? (v2 is null);
	public static bool operator ==(ZobristGrid2<T> v1, ZobristGrid2<T> v2) => Equals(v1, v2);
	public static bool operator !=(ZobristGrid2<T> v1, ZobristGrid2<T> v2) => !Equals(v1, v2);
	public override bool Equals(object obj) => Equals(obj as ZobristGrid2<T>);
	public override int GetHashCode() => hash;

	public static bool Equals(T[] a1, T[] a2)
	{
		if (a1.Length != a2.Length) return false;
		var c = EqualityComparer<T>.Default;
		for (int i = 0; i < a1.Length; ++i)
			if (!c.Equals(a1[i], a2[i])) return false;
		return true;
	}
	#endregion

	public ZobristGrid2<T> SwapRows(int i1, int i2)
	{
		var r = Clone();
		for (int j = 0; j < n2; ++j)
		{
			r[i1, j] = this[i2, j];
			r[i2, j] = this[i1, j];
		}
		return r;
	}

	public ZobristGrid2<T> SwapColumns(int j1, int j2)
	{
		var r = Clone();
		for (int i = 0; i < n1; ++i)
		{
			r[i, j1] = this[i, j2];
			r[i, j2] = this[i, j1];
		}
		return r;
	}
}
