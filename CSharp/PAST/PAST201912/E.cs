using System;
using System.Linq;

class E
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var n = h[0];
		var s = new int[h[1]].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var f = new bool[n, n];
		foreach (var q in s)
		{
			var u = q[1] - 1;
			if (q[0] == 1)
			{
				f[u, q[2] - 1] = true;
			}
			else if (q[0] == 2)
			{
				for (int i = 0; i < n; i++)
					if (f[i, u]) f[u, i] = true;
			}
			else
			{
				var t = Enumerable.Range(0, n).Where(x => f[u, x]).ToArray();
				foreach (var x in t)
					for (int k = 0; k < n; k++)
						if (k != u && f[x, k]) f[u, k] = true;
			}
		}
		Console.WriteLine(string.Join("\n", Enumerable.Range(0, n).Select(i => string.Join("", Enumerable.Range(0, n).Select(j => f[i, j] ? "Y" : "N")))));
	}
}
