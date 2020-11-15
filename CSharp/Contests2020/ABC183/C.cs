using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var t = Array.ConvertAll(new bool[n], _ => Read());

		var rn1 = Enumerable.Range(1, n - 1).ToArray();
		var rn2 = Enumerable.Range(1, n - 2).ToArray();

		var r = 0;
		Permutation(rn1, n - 1, p =>
		{
			var sum = rn2.Sum(i => t[p[i - 1]][p[i]]);
			sum += t[0][p[0]];
			sum += t[p.Last()][0];
			if (sum == k) r++;
		});
		Console.WriteLine(r);
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
