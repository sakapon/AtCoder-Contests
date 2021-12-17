using System;
using System.Collections.Generic;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var h = Console.ReadLine().Split();
		var s = h[0];
		var n = s.Length;
		var k = int.Parse(h[1]);

		var set = new HashSet<string>();

		Permutation(s.ToCharArray(), n, p =>
		{
			set.Add(new string(p));
		});

		var r = new string[set.Count];
		set.CopyTo(r);
		Array.Sort(r, StringComparer.Ordinal);
		return r[k - 1];
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
