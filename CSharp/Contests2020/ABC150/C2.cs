using System;
using System.Linq;

class C2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		values = Enumerable.Range(1, n).ToArray();
		int i = 0, j = 0, c = 0;
		action = () =>
		{
			++c;
			if (p.SequenceEqual(a)) i = c;
			if (p.SequenceEqual(b)) j = c;
		};
		Perm(n);
		Console.WriteLine(Math.Abs(i - j));
	}

	static int[] values;
	static Action action;
	static int[] p;
	static bool[] u;
	static void Perm(int r)
	{
		p = new int[r];
		u = new bool[values.Length];
		if (r > 0) Dfs(0); else action();
	}
	static void Dfs(int i)
	{
		for (int j = 0; j < u.Length; ++j)
		{
			if (u[j]) continue;
			p[i] = values[j];
			u[j] = true;
			if (i + 1 < p.Length) Dfs(i + 1); else action();
			u[j] = false;
		}
	}
}
