using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int p, int e) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ =>
		{
			var m = int.Parse(Console.ReadLine());
			return Array.ConvertAll(new bool[m], _ => Read2());
		});

		// 各素数に対する最大指数
		var e_max = new Dictionary<int, int>();
		// 各素数に対する単独最大指数を持つ数の ID
		var e_argmax = new Dictionary<int, int>();

		for (int i = 0; i < n; i++)
		{
			foreach (var (p, e) in a[i])
			{
				if (e_max.ContainsKey(p))
				{
					if (e_max[p] == e)
					{
						e_argmax.Remove(p);
					}
					else if (e_max[p] < e)
					{
						e_max[p] = e;
						e_argmax[p] = i;
					}
				}
				else
				{
					e_max[p] = e;
					e_argmax[p] = i;
				}
			}
		}
		return Math.Min(n, e_argmax.Values.Distinct().Count() + 1);
	}
}
