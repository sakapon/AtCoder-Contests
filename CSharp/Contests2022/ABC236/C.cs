using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var s = Console.ReadLine().Split();
		var t = Console.ReadLine().Split();

		var q = new Queue<string>(t);

		var r = s.Select(si =>
		{
			if (q.Peek() == si)
			{
				q.Dequeue();
				return true;
			}
			else
			{
				return false;
			}
		});
		return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
	}
}
