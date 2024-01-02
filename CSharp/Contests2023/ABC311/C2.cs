using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var v = 1;
		var u = new bool[n + 1];
		var l = new List<int>();

		for (; !u[v]; v = a[v - 1])
		{
			u[v] = true;
			l.Add(v);
		}

		var r = l.SkipWhile(x => x != v).ToArray();
		return $"{r.Length}\n" + string.Join(" ", r);
	}
}
