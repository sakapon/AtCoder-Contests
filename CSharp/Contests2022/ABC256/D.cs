using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var q = new Stack<int>();
		q.Push(-1);
		Array.Sort(ps);

		foreach (var (l, r) in ps)
		{
			if (q.Peek() < l)
			{
				q.Push(l);
				q.Push(r);
			}
			else
			{
				if (q.Peek() < r)
				{
					q.Pop();
					q.Push(r);
				}
			}
		}

		var segs = q.ToArray();
		Array.Reverse(segs);
		return string.Join("\n", Enumerable.Range(1, segs.Length / 2).Select(i => $"{segs[2 * i - 1]} {segs[2 * i]}"));
	}
}
