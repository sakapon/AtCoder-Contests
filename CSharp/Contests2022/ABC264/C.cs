using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h1, w1) = Read2();
		var a = Array.ConvertAll(new bool[h1], _ => Read());
		var (h2, w2) = Read2();
		var b = Array.ConvertAll(new bool[h2], _ => Read());

		Combination(Enumerable.Range(0, h1).ToArray(), h2, r =>
		{
			Combination(Enumerable.Range(0, w1).ToArray(), w2, c =>
			{
				var d = Array.ConvertAll(new bool[h2], _ => new int[w2]);
				for (int i = 0; i < h2; i++)
				{
					for (int j = 0; j < w2; j++)
					{
						if (b[i][j] != a[r[i]][c[j]]) return;
					}
				}

				Console.WriteLine("Yes");
				Environment.Exit(0);
			});
		});

		Console.WriteLine("No");
	}

	public static void Combination<T>(T[] values, int r, Action<T[]> action)
	{
		var p = new T[r];

		Action<int, int> Dfs = null;
		Dfs = (i, j0) =>
		{
			for (int j = j0; j < values.Length; ++j)
			{
				p[i] = values[j];
				if (i + 1 < r) Dfs(i + 1, j + 1); else action(p);
			}
		};
		if (r > 0) Dfs(0, 0); else action(p);
	}
}
