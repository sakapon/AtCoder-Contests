using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());

		var names = es.SelectMany(e => e).Distinct().ToArray();
		var map = Enumerable.Range(0, names.Length).ToDictionary(i => names[i]);

		var uf = new UF(names.Length);

		foreach (var e in es)
		{
			uf.Unite(map[e[0]], map[e[1]]);
		}

		Console.WriteLine(uf.GroupsCount);
		Console.WriteLine(string.Join(" ", uf.ToGroups().Select(g => g.Length).OrderBy(x => x)));
	}
}

class UF
{
	int[] p, sizes;
	public int GroupsCount;
	public UF(int n)
	{
		p = Enumerable.Range(0, n).ToArray();
		sizes = Array.ConvertAll(p, _ => 1);
		GroupsCount = n;
	}

	public int GetRoot(int x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
	public int GetSize(int x) => sizes[GetRoot(x)];

	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public bool Unite(int x, int y)
	{
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

		// 要素数が大きいほうのグループにマージします。
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
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
}
