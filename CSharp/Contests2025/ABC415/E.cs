class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = new SeqGrid<int>(Array.ConvertAll(new bool[h], _ => Read()));
		var p = Read();

		return First(0, 1L << 50, Check);

		bool Check(long x)
		{
			var dp = new SeqGrid<long>(h, w, long.MinValue);
			dp[0, 0] = x;

			Point pr = (0, 1);
			Point pd = (1, 0);
			Point delta = (-1, 1);
			Point end = (h - 1, w - 1);

			for (int k = 0; k < h + w - 1; k++)
			{
				var si = Math.Min(h - 1, k);
				for (Point c = (si, k - si); c.i >= 0; c += delta)
				{
					var (i, j) = c;
					if (j >= w) break;
					if (dp[c] == long.MinValue) continue;

					var nv = dp[i, j] + a[i, j] - p[k];
					dp[c] = nv;
					if (nv < 0) continue;

					if (i + 1 < h) dp[i + 1, j] = Math.Max(dp[i + 1, j], nv);
					if (j + 1 < w) dp[i, j + 1] = Math.Max(dp[i, j + 1], nv);
					if (c == end) return nv >= 0;
				}
			}
			return dp[end] >= 0;
		}
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}

public static class ValuesEx
{
	public static int Chmax(ref this int x, int v) => x < v ? x = v : x;
	public static int Chmin(ref this int x, int v) => x > v ? x = v : x;
	public static long Chmax(ref this long x, long v) => x < v ? x = v : x;
	public static long Chmin(ref this long x, long v) => x > v ? x = v : x;
}

public record struct Point(int i, int j)
{
	public static Point Parse(string s)
	{
		var p = s.Split(' ', ',');
		return new(int.Parse(p[0]), int.Parse(p[1]));
	}
	public static implicit operator Point((int i, int j) v) => new(v.i, v.j);

	#region Unary Operators
	public static Point operator +(Point v) => v;
	public static Point operator -(Point v) => new(-v.i, -v.j);
	#endregion

	#region Binary Operators
	public static Point operator +(Point v1, Point v2) => new(v1.i + v2.i, v1.j + v2.j);
	public static Point operator -(Point v1, Point v2) => new(v1.i - v2.i, v1.j - v2.j);
	public static Point operator *(int c, Point v) => new(v.i * c, v.j * c);
	public static Point operator *(Point v, int c) => new(v.i * c, v.j * c);
	public static Point operator /(Point v, int c) => new(v.i / c, v.j / c);
	#endregion
}

public class SeqGrid<T> : IEnumerable<ArraySegment<T>>
{
	public readonly int h, w;
	public readonly T[] a;
	public SeqGrid(int _h, int _w, T[] _a = null) => (h, w, a) = (_h, _w, _a ?? new T[_h * _w]);
	public SeqGrid(int _h, int _w, T iv) : this(_h, _w, default(T[])) => Array.Fill(a, iv);
	public SeqGrid(T[][] g) : this(g.Length, g.Length > 0 ? g[0].Length : 0, g.SelectMany(r => r).ToArray()) { }

	public ref T this[int i, int j]
	{
		get => ref a[w * i + j];
	}
	public ref T this[Point p]
	{
		get => ref a[w * p.i + p.j];
	}
	public ArraySegment<T> this[int i] => new(a, w * i, w);

	public T[] GetRow(int i) => a[(w * i)..(w * (i + 1))];
	public T[] GetColumn(int j)
	{
		var r = new T[h];
		for (int i = 0; i < h; ++i) r[i] = a[w * i + j];
		return r;
	}
	public T[][] GetRows()
	{
		var r = new T[h][];
		for (int i = 0; i < h; ++i) r[i] = GetRow(i);
		return r;
	}
	public T[][] GetColumns()
	{
		var r = new T[w][];
		for (int j = 0; j < w; ++j) r[j] = GetColumn(j);
		return r;
	}

	public void Fill(T v) => Array.Fill(a, v);
	public void Fill(int i, T v) => Array.Fill(a, v, w * i, w);
	public void Clear() => Array.Clear(a, 0, a.Length);
	public SeqGrid<T> Clone() => new(h, w, (T[])a.Clone());

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<ArraySegment<T>> GetEnumerator() { for (int i = 0; i < h; ++i) yield return this[i]; }
}
