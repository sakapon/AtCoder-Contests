using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var vs = a.Select((x, i) => i + x).Concat(a.Select((x, i) => i - x)).Distinct().OrderBy(v => v).ToArray();
		var map = Enumerable.Range(0, vs.Length).ToDictionary(i => vs[i]);

		var r = 0L;
		var c = new int[vs.Length];
		for (int i = 0; i < n; i++)
		{
			r += c[map[i - a[i]]];
			c[map[i + a[i]]]++;
		}
		Console.WriteLine(r);
	}
}
