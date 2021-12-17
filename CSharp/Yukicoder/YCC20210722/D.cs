using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3L();

		var max = m - (n - 1) * (n - 2) / 2;
		var st = new Stack<long>(Enumerable.Range(0, (int)n - 1).Select(i => (long)i));
		st.Push(max);

		var r = Array.ConvertAll(new bool[n], _ => -1L);

		for (int i = 0; i < n && k > 0; i++)
		{
			if (k <= n - 1 - i)
			{
				r[n - 1 - k] = st.Pop();
				k = 0;
			}
			else
			{
				r[i] = st.Pop();
				k -= n - 1 - i;
			}
		}

		var q = new Queue<long>(st.Reverse());

		for (int i = 0; i < n; i++)
			if (r[i] == -1)
				r[i] = q.Dequeue();

		return string.Join("\n", r);
	}
}
