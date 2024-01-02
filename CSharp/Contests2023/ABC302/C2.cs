using System;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var map = new bool[n, n];
		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
				map[i, j] = IsNext(s[i], s[j]);

		var u = new bool[n];

		for (int sv = 0; sv < n; sv++)
		{
			if (DFS(sv, 1)) return true;
		}
		return false;

		bool IsNext(string s, string t)
		{
			var c = 0;
			for (int i = 0; i < m; i++)
				if (s[i] != t[i]) c++;
			return c == 1;
		}

		bool DFS(int v, int d)
		{
			if (d == n) return true;
			u[v] = true;

			for (int nv = 0; nv < n; nv++)
			{
				if (!map[v, nv]) continue;
				if (u[nv]) continue;
				if (DFS(nv, d + 1)) return true;
			}
			u[v] = false;
			return false;
		}
	}
}
