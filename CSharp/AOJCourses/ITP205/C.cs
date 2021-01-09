using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		int[] prev = null;
		var end = false;
		Permutation(a.OrderBy(x => x).ToArray(), n, p =>
		{
			if (end)
			{
				Console.WriteLine(string.Join(" ", p));
				Environment.Exit(0);
			}

			if (Enumerable.SequenceEqual(p, a))
			{
				if (prev != null) Console.WriteLine(string.Join(" ", prev));
				Console.WriteLine(string.Join(" ", p));
				end = true;
			}
			prev = (int[])p.Clone();
		});
	}

	static void Permutation<T>(T[] values, int r, Action<T[]> action)
	{
		var p = new T[r];
		var u = new bool[values.Length];

		Action<int> Dfs = null;
		Dfs = i =>
		{
			for (int j = 0; j < values.Length; ++j)
			{
				if (u[j]) continue;
				p[i] = values[j];
				u[j] = true;
				if (i + 1 < r) Dfs(i + 1); else action(p);
				u[j] = false;
			}
		};
		if (r > 0) Dfs(0); else action(p);
	}
}
