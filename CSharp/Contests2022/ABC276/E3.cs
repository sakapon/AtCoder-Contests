using System;
using System.Collections.Generic;
using System.Linq;

class E3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var si = Enumerable.Range(0, h).First(i => c[i].Contains('S'));
		var sj = c[si].IndexOf('S');
		var sv = si * w + sj;

		c[si] = c[si].Replace('S', '#');
		var uf = GetConnectivity(h, w, c);

		var svs = new List<int>();
		if (sj > 0) svs.Add(sv - 1);
		if (sj + 1 < w) svs.Add(sv + 1);
		if (si > 0) svs.Add(sv - w);
		if (si + 1 < h) svs.Add(sv + w);

		for (int i = 0; i < svs.Count; i++)
		{
			for (int j = i + 1; j < svs.Count; j++)
			{
				if (uf.AreUnited(svs[i], svs[j])) return true;
			}
		}

		return false;
	}

	// undirected
	public static UF GetConnectivity(int h, int w, string[] s, char wall = '#')
	{
		var uf = new UF(h * w);
		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				var v = i * w + j;
				if (s[i][j] == wall || s[i][j - 1] == wall) continue;
				uf.Unite(v, v - 1);
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				var v = i * w + j;
				if (s[i][j] == wall || s[i - 1][j] == wall) continue;
				uf.Unite(v, v - w);
			}
		return uf;
	}
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
