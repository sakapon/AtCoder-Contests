using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var sr = string.Join("", s.Reverse());

		Permutation(s.ToCharArray(), n, cs =>
		{
			var t = new string(cs);

			if (s == t || sr == t) return;

			Console.WriteLine(t);
			Environment.Exit(0);
		});

		Console.WriteLine("None");
	}

	public static void Permutation<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];
		var u = new bool[n];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				if (u[j]) continue;
				p[i] = values[j];
				u[j] = true;

				if (i2 < r) Dfs(i2);
				else action(p);

				u[j] = false;
			}
		}
	}
}
