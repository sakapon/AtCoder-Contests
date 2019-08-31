using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var a = read();
		int n = h[0], x = h[1];

		var bis = new List<int>();
		for (var i = 0; i < n; i++) if (a[i] == -1) bis.Add(i);
		var q = bis.Count == n ? x : a.Where(v => v != -1).Aggregate((y, z) => y ^ z) ^ x;

		if (bis.Count == 0)
		{
			if (q > 0) { Console.WriteLine(-1); return; }
		}
		else if (bis.Count == 1)
		{
			if (q > x) { Console.WriteLine(-1); return; }
			a[bis[0]] = q;
		}
		else
		{
			var p = (int)Math.Pow(2, (int)Math.Log(q, 2));
			if (p > x) { Console.WriteLine(-1); return; }
			a[bis[0]] = p;
			a[bis[1]] = p ^ q;
			for (var i = 2; i < bis.Count; i++) a[bis[i]] = 0;
		}
		Console.WriteLine(string.Join(" ", a));
	}
}
