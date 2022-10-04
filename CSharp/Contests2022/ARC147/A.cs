using System;
using System.Collections.Generic;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var a = Read();

		Array.Sort(a);
		var q = new LinkedList<int>(a);

		int k = 0;
		for (; q.Count > 1; k++)
		{
			var v = q.Last.Value % q.First.Value;
			if (v > 0) q.AddFirst(v);
			q.RemoveLast();
		}
		return k;
	}
}
