class E3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var uf = new UF(n);
		foreach (var (u, v) in es)
		{
			if (a[u - 1] == a[v - 1]) uf.Unite(u - 1, v - 1);
		}

		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var (u, v) in es)
		{
			if (a[u - 1] == a[v - 1]) continue;
			if (a[u - 1] < a[v - 1]) map[uf.GetRoot(u - 1)].Add(uf.GetRoot(v - 1));
			else map[uf.GetRoot(v - 1)].Add(uf.GetRoot(u - 1));
		}

		var sv = uf.GetRoot(0);
		var ev = uf.GetRoot(n - 1);

		var dp = new int[n];
		dp[sv] = 1;

		var vs = Enumerable.Range(0, n).Select(uf.GetRoot).Distinct().OrderBy(v => a[v]);
		foreach (var v in vs)
		{
			if (dp[v] == 0) continue;
			foreach (var nv in map[v])
			{
				Chmax(ref dp[nv], dp[v] + 1);
			}
		}
		return dp[ev];
	}

	public static int Chmax(ref int x, int v) => x < v ? x = v : x;
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
}
