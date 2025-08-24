using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var g = new CharUnweightedGrid2(s);
		var sv = g.FindVertexId('S');
		var ev = g.FindVertexId('G');

		var r = g.ShortestByBFS(2 * sv);
		var min = Math.Min(r[2 * ev], r[2 * ev + 1]);
		if (min == long.MaxValue) return -1;
		return min;
	}
}

class CharUnweightedGrid2 : UnweightedGraph
{
	const char wall = '#';
	const char sw = '?';
	const char op = 'o';
	const char cl = 'x';
	public static (int di, int dj)[] NextsDelta { get; } = new[] { (0, -1), (0, 1), (-1, 0), (1, 0) };

	protected readonly int h, w;
	readonly string[] s;
	public CharUnweightedGrid2(string[] s) : base(s.Length * s[0].Length * 2)
	{
		h = s.Length;
		w = s[0].Length;
		this.s = s;
	}

	public int FindVertexId(char c)
	{
		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
				if (s[i][j] == c) return w * i + j;
		return -1;
	}

	public override List<int> GetEdges(int v2)
	{
		var (v, p) = (v2 / 2, v2 % 2);
		var (i, j) = (v / w, v % w);
		var l = new List<int>();
		foreach (var (di, dj) in NextsDelta)
		{
			var (ni, nj) = (i + di, j + dj);
			if (!(0 <= ni && ni < h && 0 <= nj && nj < w)) continue;
			if (s[ni][nj] == wall) continue;
			if (s[ni][nj] == cl && p == 0) continue;
			if (s[ni][nj] == op && p == 1) continue;

			var nv = w * ni + nj;
			var np = p ^ (s[ni][nj] == sw ? 1 : 0);
			l.Add(2 * nv + np);
		}
		return l;
	}
}
