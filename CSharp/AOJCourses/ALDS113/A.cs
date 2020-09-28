using System;
using System.Linq;

class A
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		var ps = new int[k].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var c = NewArray(8, 8, '.');

		Action<int, int, char, char> Replace = (x, y, before, after) =>
		{
			for (int i = 0; i < 8; i++)
			{
				if (i == x) continue;
				if (c[i][y] == before) c[i][y] = after;

				// i+j=x+y
				var j = x + y - i;
				if (0 <= j && j < 8 && c[i][j] == before) c[i][j] = after;

				// i-j=x-y
				j = -x + y + i;
				if (0 <= j && j < 8 && c[i][j] == before) c[i][j] = after;
			}
			for (int j = 0; j < 8; j++)
			{
				if (j == y) continue;
				if (c[x][j] == before) c[x][j] = after;
			}
		};

		foreach (var p in ps)
		{
			c[p[0]][p[1]] = 'Q';
			Replace(p[0], p[1], '.', '/');
		}

		Func<int, bool> Dfs = null;
		Dfs = d =>
		{
			if (d++ == 8) return true;
			var dc = (char)('0' + d);

			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
				{
					if (c[i][j] != '.') continue;

					c[i][j] = 'Q';
					Replace(i, j, '.', dc);
					if (Dfs(d)) return true;
					c[i][j] = '.';
					Replace(i, j, dc, '.');
				}
			return false;
		};

		Dfs(k);
		Console.WriteLine(string.Join("\n", c.Select(cs => string.Join("", cs.Select(x => x == 'Q' ? 'Q' : '.')))));
	}

	static T[] NewArray<T>(int n, Func<T> newItem)
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = newItem();
		return a;
	}

	static T[] NewArray<T>(int n, T v)
	{
		var a = new T[n];
		if (!Equals(v, default(T)))
			for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}

	static T[][] NewArray<T>(int n1, int n2, T v) => NewArray(n1, () => NewArray(n2, v));
}
