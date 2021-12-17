using System;
using System.Collections.Generic;
using CoderLib6.Trees;

class I
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var q = PQ<long>.Create();

		var c = 0;
		foreach (var v in a)
		{
			var x = v;
			while ((x & 1) == 0)
			{
				x >>= 1;
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
