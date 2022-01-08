using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees;

class Q006K
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine();

		var cs = new char[k];

		var q = new KeyedPriorityQueue<int, char>(i => s[i]);
		for (int i = 0; i < n - k; i++)
		{
			q.Push(i);
		}

		var t = -1;
		for (int i = n - k; i < n; i++)
		{
			q.Push(i);
			while (q.Peek() < t) q.Pop();

			t = q.Pop();
			cs[i - n + k] = s[t];
		}

		return new string(cs);
	}
}
