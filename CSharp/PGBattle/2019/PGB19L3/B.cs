using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		// 第1段の人数
		var c = (n + 1) / 2;

		var r = new int[n];
		var tl = 1;
		var tr = n;

		for (int i = 0; i < c; i++)
		{
			if (s[i] == 'L')
			{
				r[i] = tl;
				tl += 2;
			}
			else
			{
				r[i] = tr;
				tr -= 2;
			}
		}

		var rn = Enumerable.Range(1, n).ToArray();
		var ll = new LinkedList<int>(rn.Except(r[..c]));

		for (int i = c; i < n; i++)
		{
			if (s[i] == 'L')
			{
				r[i] = ll.First.Value;
				ll.RemoveFirst();
			}
			else
			{
				r[i] = ll.Last.Value;
				ll.RemoveLast();
			}
		}

		return string.Join("\n", r);
	}
}
