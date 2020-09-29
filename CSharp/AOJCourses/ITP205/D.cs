using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		Permutation(Enumerable.Range(1, n).ToArray(), n, p => Console.WriteLine(string.Join(" ", p)));
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
