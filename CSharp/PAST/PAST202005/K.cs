using System;
using System.Linq;

class K
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], q = h[1];

		var desks = Enumerable.Range(0, n + 1).ToArray();
		var nexts = Enumerable.Repeat(-1, n + 1).ToArray();

		for (int i = 0; i < q; i++)
		{
			var s = Read();
			int f = s[0], t = s[1], x = s[2];

			var temp = desks[t];
			desks[t] = desks[f];
			desks[f] = nexts[x];
			nexts[x] = temp;
		}

		var r = new int[n + 1];
		for (int d = 1; d <= n; d++)
		{
			var c = desks[d];
			while (true)
			{
				if (c == -1) break;
				r[c] = d;
				c = nexts[c];
			}
		}
		Console.WriteLine(string.Join("\n", r.Skip(1)));
	}
}
