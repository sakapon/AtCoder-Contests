class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Read());

		var sv = new Grid2<byte>(h, w, s.SelectMany(x => x).Select(x => (byte)x).ToArray());
		var ev = new Grid2<byte>(h, w, Enumerable.Range(1, h * w).Select(x => (byte)x).ToArray());

		var d1 = ShortestByBFS(GetNexts, sv, null, 10);
		var d2 = ShortestByBFS(GetNexts, ev, null, 10);

		var vs = d1.Keys.ToHashSet();
		vs.IntersectWith(d2.Keys);
		if (vs.Count == 0) return -1;
		return vs.Min(v => d1[v] + d2[v]);

		Grid2<byte>[] GetNexts(Grid2<byte> v)
		{
			var l = new List<Grid2<byte>>();

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

public class Grid2<T> : IEnumerable<ArraySegment<T>>, IEquatable<Grid2<T>>
{
	public readonly int n1, n2;
	public readonly T[] a;
	public Grid2(int _n1, int _n2, T[] _a = null) => (n1, n2, a) = (_n1, _n2, _a ?? new T[_n1 * _n2]);
	public Grid2(int _n1, int _n2, T iv) : this(_n1, _n2, default(T[])) => Array.Fill(a, iv);

	public T this[int i, int j]
	{
		get => a[n2 * i + j];
		set => a[n2 * i + j] = value;
	}
	public ArraySegment<T> this[int i] => new ArraySegment<T>(a, n2 * i, n2);
	public T[] ToArray(int i) => a[(n2 * i)..(n2 * (i + 1))];

	public T[] GetColumn(int j)
	{
		var r = new T[n1];
		for (int i = 0; i < n1; ++i) r[i] = a[n2 * i + j];
		return r;
	}
	public IEnumerable<T[]> GetColumns() { for (int j = 0; j < n2; ++j) yield return GetColumn(j); }

	public void Fill(int i, T v) => Array.Fill(a, v, n2 * i, n2);
	public void Fill(T v) => Array.Fill(a, v);
	public void Clear() => Array.Clear(a, 0, a.Length);
	public Grid2<T> Clone() => new Grid2<T>(n1, n2, (T[])a.Clone());

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<ArraySegment<T>> GetEnumerator() { for (int i = 0; i < n1; ++i) yield return this[i]; }

	#region Equality Operators
	public bool Equals(Grid2<T> other) => !(other is null) && Equals(a, other.a);
	public static bool Equals(Grid2<T> v1, Grid2<T> v2) => v1?.Equals(v2) ?? (v2 is null);
	public static bool operator ==(Grid2<T> v1, Grid2<T> v2) => Equals(v1, v2);
	public static bool operator !=(Grid2<T> v1, Grid2<T> v2) => !Equals(v1, v2);
	public override bool Equals(object obj) => Equals(obj as Grid2<T>);
	public override int GetHashCode() => GetHashCode(a);

	public static bool Equals(T[] a1, T[] a2)
	{
		if (a1.Length != a2.Length) return false;
		var c = EqualityComparer<T>.Default;
		for (int i = 0; i < a1.Length; ++i)
			if (!c.Equals(a1[i], a2[i])) return false;
		return true;
	}
	public static int GetHashCode(T[] a)
	{
		var h = 0;
		for (int i = 0; i < a.Length; ++i) h = h * 10000019 + a[i].GetHashCode();
		return h;
	}
	#endregion
}
