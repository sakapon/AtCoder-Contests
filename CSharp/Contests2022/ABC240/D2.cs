using System;
using System.Collections.Generic;

class D2
{
	class Ball
	{
		public int v, c;
	}

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = new int[n];
		var count = 0;

		var q = new Stack<Ball>();
		q.Push(new Ball());

		for (int i = 0; i < n; i++)
		{
			var v = a[i];
			var b = q.Peek();

			if (v == b.v)
			{
				if (v == b.c + 1)
				{
					count -= v;
					q.Pop();
				}
				else
				{
					b.c++;
				}
			}
			else
			{
				q.Push(new Ball { v = v, c = 1 });
			}

			r[i] = ++count;
		}

		return string.Join("\n", r);
	}
}
