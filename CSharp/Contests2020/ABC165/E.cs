using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], m = h[1];

		var map = new List<(int a, int b)>();

		var (l, r) = (1, m);
		while (map.Count < m && l < r) map.Add((l++, r--));
		(l, r) = (m + 1, 2 * m + 1);
		while (map.Count < m && l < r) map.Add((l++, r--));

		Console.WriteLine(string.Join("\n", map.Select(t => $"{t.a} {t.b}")));
	}
}
