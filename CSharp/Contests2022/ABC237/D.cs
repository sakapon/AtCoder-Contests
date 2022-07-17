using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var l = new LinkedList<int>();
		var node = l.AddFirst(0);

		for (int i = 1; i <= n; i++)
		{
			if (s[i - 1] == 'L')
			{
				node = l.AddBefore(node, i);
			}
			else
			{
				node = l.AddAfter(node, i);
			}
		}

		return string.Join(" ", l);
	}
}
