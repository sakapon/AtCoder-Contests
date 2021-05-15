using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var r = 0;

		Power(Enumerable.Range(0, 10).ToArray(), 4, p =>
		{
			for (int i = 0; i < 10; i++)
			{
				if (s[i] == 'o')
				{
					if (Array.IndexOf(p, i) == -1) return;
				}
				else if (s[i] == 'x')
				{
					if (Array.IndexOf(p, i) != -1) return;
				}
			}

			r++;
		});

		return r;
	}

	static void Power<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2);
				else action(p);
			}
		}
	}
}
