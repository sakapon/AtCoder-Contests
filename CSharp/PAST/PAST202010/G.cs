using System;
using System.Linq;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine().ToCharArray());

		var sum = s.Sum(r => r.Count(c => c == '.')) + 1;

		var r = 0;
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++)
			{
				if (s[i][j] == '.') continue;

				s[i][j] = '.';
				r += AreUnited(i, j) ? 1 : 0;
				s[i][j] = '#';
			}
		}
		return r;

		bool AreUnited(int ti, int tj)
		{
			var uf = new UF(n * m);
			int ToId(int x, int y) => x * m + y;

			for (int i = 0; i < n; i++)
			{
				for (int j = 1; j < m; j++)
				{
					if (s[i][j - 1] == '.' && s[i][j] == '.')
						uf.Unite(ToId(i, j - 1), ToId(i, j));
				}
			}

			for (int j = 0; j < m; j++)
			{
				for (int i = 1; i < n; i++)
				{
					if (s[i - 1][j] == '.' && s[i][j] == '.')
						uf.Unite(ToId(i - 1, j), ToId(i, j));
				}
			}

			return uf.GetSize(ToId(ti, tj)) == sum;
		}
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

class UF<T> : UF
{
	T[] a;
	// (parent, child) => result
	Func<T, T, T> MergeData;
	public UF(int n, Func<T, T, T> merge, T[] a0) : base(n)
	{
		a = a0;
		MergeData = merge;
	}

	public T GetValue(int x) => a[GetRoot(x)];
	protected override void Merge(int x, int y)
	{
		base.Merge(x, y);
		a[x] = MergeData(a[x], a[y]);
	}
}
