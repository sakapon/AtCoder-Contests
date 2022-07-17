using System;
using System.Collections.Generic;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = new int[n];

		var q = new Stack<(int v, int c)>();
		q.Push((-1, 0));

		for (int i = 0; i < n; i++)
		{
			var v = a[i];
			var (v0, c0) = q.Peek();

			if (v == v0)
			{
				if (v == c0 + 1)
				{
					while (c0-- > 0)
					{
						q.Pop();
					}
				}
				else
				{
					q.Push((v, c0 + 1));
				}
			}
			else
			{
				q.Push((v, 1));
			}

			r[i] = q.Count - 1;
		}

		return string.Join("\n", r);
	}
}
