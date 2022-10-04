using System;
using System.Linq;
using System.Text;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();

		var sb = new StringBuilder();
		Combination(Enumerable.Range(1, m).ToArray(), n, p => sb.AppendLine(string.Join(" ", p)));
		Console.Write(sb);
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
