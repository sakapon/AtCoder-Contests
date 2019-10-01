using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();

		var p = Enumerable.Range(0, h[0]).ToArray();
		Func<int, int> root = i =>
		{
			var x = p[i];
			while (p[x] != x) x = p[x];
			return p[i] = x;
		};
		Func<int, int, bool> eq = (i, j) => root(i) == root(j);

		foreach (var q in new int[h[1]].Select(_ => read()))
		{
			if (q[0] == 0)
			{
				if (eq(q[1], q[2])) continue;
				p[p[q[2]]] = p[p[q[1]]];
			}
			else Console.WriteLine(eq(q[1], q[2]) ? "Yes" : "No");
		}
	}
}
