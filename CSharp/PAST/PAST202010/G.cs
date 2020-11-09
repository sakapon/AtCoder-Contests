using System;
using System.Linq;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var s = Array.ConvertAll(new int[n], _ => Console.ReadLine().ToCharArray());

		var r = 0;
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++)
			{
				if (s[i][j] == '.') continue;

				s[i][j] = '.';
				r += AreUnited(n, m, s, i, j) ? 1 : 0;
				s[i][j] = '#';
			}
		}
		Console.WriteLine(r);
	}

	static bool AreUnited(int n, int m, char[][] s, int ti, int tj)
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

		var sum = s.Sum(r => r.Count(c => c == '.'));
		var root = uf.GetRoot(ToId(ti, tj));
		return Enumerable.Range(0, n * m).Count(v => uf.GetRoot(v) == root) == sum;
	}
}

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	public int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
}
