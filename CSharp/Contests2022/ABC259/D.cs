using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var (sx, sy, tx, ty) = Read4L();
		var cs = Array.ConvertAll(new bool[n], _ => Read3L());

		var si = FindCircle(sx, sy);
		var ti = FindCircle(tx, ty);

		var uf = new UF(n);

		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				if (Intersect(cs[i], cs[j]))
				{
					uf.Unite(i, j);
				}
			}
		}
		return uf.AreUnited(si, ti);

		int FindCircle(long x, long y)
		{
			for (int i = 0; i < n; i++)
			{
				var (x0, y0, r) = cs[i];
				if ((x - x0) * (x - x0) + (y - y0) * (y - y0) == r * r)
				{
					return i;
				}
			}
			return -1;
		}
	}

	static bool Intersect((long x, long y, long r) c1, (long x, long y, long r) c2)
	{
		var dx = c1.x - c2.x;
		var dy = c1.y - c2.y;
		var d2 = dx * dx + dy * dy;

		if ((c1.r + c2.r) * (c1.r + c2.r) < d2) return false;
		return (c1.r - c2.r) * (c1.r - c2.r) <= d2;
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
