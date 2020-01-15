using System;
using System.Linq;

class G
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n - 1].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var M = int.MinValue;
		Perm(new int[n], 0, g =>
		{
			var r = 0;
			for (int i = 0; i < n; i++)
				for (int j = i + 1; j < n; j++)
					if (g[i] == g[j]) r += a[i][j - i - 1];
			M = Math.Max(M, r);
		});
		Console.WriteLine(M);
	}

	static void Perm(int[] g, int i, Action<int[]> action)
	{
		for (int x = 0; x < 3; x++)
		{
			g[i] = x;
			if (i == g.Length - 1) action(g);
			else Perm(g, i + 1, action);
		}
	}
}
