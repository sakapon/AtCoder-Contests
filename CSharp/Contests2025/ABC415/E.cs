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

		var dp = new SeqGrid<long>(h + 1, w + 1);
		Point ep = (h - 1, w - 1);
		Point delta = (-1, 1);
		return First(0, 1L << 50, Check);

		bool Check(long x)
		{
			dp.Fill(long.MinValue);
			dp[0, 0] = x;

			for (int k = 0; k < h + w - 1; k++)
			{
				var off = Math.Max(k - h + 1, 0);
				for (Point c = (k, 0) + delta * off; c.i >= 0 && c.j < w; c += delta)
				{
					var (i, j) = c;
					if (dp[i, j] < 0) continue;

					dp[i, j] += a[i, j] - p[k];
					if (dp[i, j] < 0) continue;

					dp[i + 1, j].Chmax(dp[i, j]);
					dp[i, j + 1].Chmax(dp[i, j]);
				}
			}
			return dp[ep] >= 0;
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
	public static int Chmax(this ref int x, int v) => x < v ? x = v : x;
	public static int Chmin(this ref int x, int v) => x > v ? x = v : x;
	public static T Chmax<T>(this ref T x, T v) where T : struct, IComparable<T> => x.CompareTo(v) < 0 ? x = v : x;
	public static T Chmin<T>(this ref T x, T v) where T : struct, IComparable<T> => x.CompareTo(v) > 0 ? x = v : x;
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
	readonly int h, w;
	readonly T[] a;
	public int Height => h;
	public int Width => w;
	public T[] Raw => a;

	public SeqGrid(int _h, int _w, T[] _a = null) => (h, w, a) = (_h, _w, _a ?? new T[_h * _w]);
	public SeqGrid(int _h, int _w, T iv) : this(_h, _w, default(T[])) => Array.Fill(a, iv);
	public SeqGrid(T[][] g) : this(g.Length, g.Length > 0 ? g[0].Length : 0, g.SelectMany(r => r).ToArray()) { }

	public ref T this[int i, int j] => ref a[w * i + j];
	public ref T this[Point p] => ref a[w * p.i + p.j];
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

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<ArraySegment<T>> GetEnumerator() { for (int i = 0; i < h; ++i) yield return this[i]; }

	public void Fill(T v) => Array.Fill(a, v);
	public void FillRow(int i, T v) => Array.Fill(a, v, w * i, w);
	public void FillColumn(int j, T v) { for (int i = 0; i < h; ++i) a[w * i + j] = v; }
	public void Clear() => Array.Clear(a, 0, a.Length);
	public SeqGrid<T> Clone() => new(h, w, (T[])a.Clone());

	public bool IsInside(int i, int j) => 0 <= i && i < h && 0 <= j && j < w;
	public bool IsInside(Point p) => IsInside(p.i, p.j);
}
