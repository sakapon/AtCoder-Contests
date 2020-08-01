using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select((s, id) => (id, v: int.Parse(s))).OrderBy(x => x.v).ToArray();
		var b = Console.ReadLine().Split().Select(int.Parse).OrderBy(x => x).ToArray();

		var r = new int[n + 1];
		int t;
		r[a[n].id] = t = a.Zip(b, (p, q) => Math.Max(p.v - q, 0)).Max();
		for (int i = n - 1; i >= 0; i--)
			r[a[i].id] = t = Math.Max(t, Math.Max(a[i + 1].v - b[i], 0));

		Console.WriteLine(string.Join(" ", r));
	}
}
