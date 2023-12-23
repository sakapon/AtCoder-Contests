class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var n = h * w;
		var sseq = new SeqArray2<bool>(h, w, s.SelectMany(r => r.Select(c => c == '#')).ToArray());
		int ToVertexId(int i, int j) => w * i + j;

		var uf = new UF(h * w);
		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				var v = w * i + j;
				if (sseq.a[v] && sseq.a[v - 1]) uf.Unite(v, v - 1);
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				var v = w * i + j;
				if (sseq.a[v] && sseq.a[v - w]) uf.Unite(v, v - w);
			}

		var red = 0;
		var delta = 0L;
		var l = new List<int>();

		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
			{
				if (sseq[i, j]) continue;

				red++;

				l.Clear();
				if (i > 0 && sseq[i - 1, j]) l.Add(uf.GetRoot(ToVertexId(i - 1, j)));
				if (j > 0 && sseq[i, j - 1]) l.Add(uf.GetRoot(ToVertexId(i, j - 1)));
				if (i + 1 < h && sseq[i + 1, j]) l.Add(uf.GetRoot(ToVertexId(i + 1, j)));
				if (j + 1 < w && sseq[i, j + 1]) l.Add(uf.GetRoot(ToVertexId(i, j + 1)));
				delta += 1 - l.Distinct().Count();
			}

		var red_inv = MInv(red);
		return ((long)(uf.GroupsCount - red) * red + delta) % M * red_inv % M;
	}

	const long M = 998244353;
	// 0^0 は未定義
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);
}

public class SeqArray2<T>
{
	public readonly int n1, n2;
	public readonly T[] a;
	public SeqArray2(int _n1, int _n2, T[] _a = null) => (n1, n2, a) = (_n1, _n2, _a ?? new T[_n1 * _n2]);
	public SeqArray2(int _n1, int _n2, T iv) : this(_n1, _n2, default(T[])) => Array.Fill(a, iv);

	public T this[int i, int j]
	{
		get => a[n2 * i + j];
		set => a[n2 * i + j] = value;
	}
	public T[] this[int i] => a[(n2 * i)..(n2 * (i + 1))];
	public ArraySegment<T> GetSegment(int i) => new ArraySegment<T>(a, n2 * i, n2);

	public void Fill(T v) => Array.Fill(a, v);
	public void Fill(int i, T v) => Array.Fill(a, v, n2 * i, n2);
}

public class UF
{
	int[] p, sizes;
	public int GroupsCount { get; private set; }

	public UF(int n)
	{
		p = Array.ConvertAll(new bool[n], _ => -1);
		sizes = Array.ConvertAll(p, _ => 1);
		GroupsCount = n;
	}

	public int GetRoot(int x) => p[x] == -1 ? x : p[x] = GetRoot(p[x]);
	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public int GetSize(int x) => sizes[GetRoot(x)];

	public bool Unite(int x, int y)
	{
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

		if (sizes[x] < sizes[y]) Merge(y, x);
		else Merge(x, y);
		return true;
	}
	protected virtual void Merge(int x, int y)
	{
		p[y] = x;
		sizes[x] += sizes[y];
		--GroupsCount;
	}
	public ILookup<int, int> ToGroups() => Enumerable.Range(0, p.Length).ToLookup(GetRoot);
}
