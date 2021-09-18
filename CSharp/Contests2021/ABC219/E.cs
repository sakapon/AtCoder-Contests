using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = Array.ConvertAll(new bool[4], _ => Read());

		const int n = 4;

		// 外周を含むサイズ。
		var (h2, w2) = (4, 4);
		Enclose(ref h2, ref w2, ref a, 0);

		int ToId(int i, int j) => i * n + j;
		int ToId2(int i2, int j2) => i2 * w2 + j2;
		//Point FromId(int id) => new Point(id / w, id % w);

		bool IsIn(int i2, int j2, bool[] b)
		{
			if (i2 == 0 || i2 == h2 - 1) return false;
			if (j2 == 0 || j2 == w2 - 1) return false;
			return b[ToId(i2 - 1, j2 - 1)];
		}

		var r = 0;

		AllBoolCombination(n * n, b =>
		{
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					if (a[i + 1][j + 1] == 1)
					{
						var id = ToId(i, j);
						if (!b[id]) return false;
					}
				}
			}

			var uf = new UF(h2 * w2);
			for (int i = 0; i < h2; i++)
			{
				for (int j = 1; j < w2; j++)
				{
					if (IsIn(i, j, b) == IsIn(i, j - 1, b))
					{
						var id = ToId2(i, j);
						uf.Unite(id, id - 1);
					}
				}
			}
			for (int j = 0; j < w2; j++)
			{
				for (int i = 1; i < h2; i++)
				{
					if (IsIn(i, j, b) == IsIn(i - 1, j, b))
					{
						var id = ToId2(i, j);
						uf.Unite(id, id - w2);
					}
				}
			}

			if (uf.GroupsCount == 2)
			{
				r++;
			}

			return false;
		});

		return r;
	}

	public static void AllBoolCombination(int n, Func<bool[], bool> action)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		for (int x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
			if (action(b)) break;
		}
	}

	public static void Enclose<T>(ref int height, ref int width, ref T[][] a, T value, int delta = 1)
	{
		var (h, w) = (height + 2 * delta, width + 2 * delta);
		var (li, ri) = (Math.Max(0, -delta), Math.Min(height, height + delta));
		var (lj, rj) = (Math.Max(0, -delta), Math.Min(width, width + delta));

		var t = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => value));
		for (int i = li; i < ri; ++i)
			for (int j = lj; j < rj; ++j)
				t[delta + i][delta + j] = a[i][j];
		(height, width, a) = (h, w, t);
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
