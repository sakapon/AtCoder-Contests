using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var qs = Array.ConvertAll(new bool[m], _ => { Console.ReadLine(); return new Queue<int>(Read()); });

		var end = 0;
		var q2 = new Queue<int>();
		var u1 = new bool[n + 1];

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int i = 0; i < m; i++)
		{
			foreach (var color in qs[i])
			{
				map[color].Add(i);
			}
		}

		void Increment(int qi, int color)
		{
			if (u1[color])
			{
				q2.Enqueue(color);
			}
			else
			{
				u1[color] = true;
			}
		}

		for (int qi = 0; qi < m; qi++)
		{
			Increment(qi, qs[qi].Peek());
		}

		while (q2.Count > 0)
		{
			end++;
			var color = q2.Dequeue();

			foreach (var qi in map[color])
			{
				var q = qs[qi];
				q.Dequeue();
				if (q.Count > 0) Increment(qi, q.Peek());
			}
		}

		return end == n;
	}
}
