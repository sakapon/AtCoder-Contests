class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var n = h * w;
		var b = s.SelectMany(r => r.Select(c => c == '#')).ToArray();

		var uf = new UF(n);
		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				var v = w * i + j;
				if (b[v] && b[v - 1]) uf.Unite(v, v - 1);
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				var v = w * i + j;
				if (b[v] && b[v - w]) uf.Unite(v, v - w);
			}

		var red = 0;
		var delta = 0;
		var l = new List<int>();

		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
			{
				var v = w * i + j;
				if (b[v]) continue;

				red++;

				if (i > 0 && b[v - w]) l.Add(uf.GetRoot(v - w));
				if (j > 0 && b[v - 1]) l.Add(uf.GetRoot(v - 1));
				if (i + 1 < h && b[v + w]) l.Add(uf.GetRoot(v + w));
				if (j + 1 < w && b[v + 1]) l.Add(uf.GetRoot(v + 1));
				delta += 1 - l.Distinct().Count();
				l.Clear();
			}

		return (uf.GroupsCount - red + MInt(delta * MInv(red))) % M;
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
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
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
