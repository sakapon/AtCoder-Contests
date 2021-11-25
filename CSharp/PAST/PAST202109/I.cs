using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.Spp;

class I
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var q = PriorityQueue<long>.Create();

		var c = 0;
		foreach (var v in a)
		{
			var x = v;
			while (x % 2 == 0)
			{
				x /= 2;
				c++;
			}
			q.Push(x);
		}

		while (c-- > 0)
		{
			var x = q.Pop();
			q.Push(x * 3);
		}

		return q.First;
	}
}
