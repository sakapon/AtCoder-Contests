using System;
using System.Collections.Generic;

class Q006S
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine();

		var r = new List<char>();

		var q = new BstPQ1<long>();
		for (int i = 0; i < n - k; i++)
		{
			q.Add(s[i] * 100000L + i);
		}

		var t = -1;
		for (int i = n - k; i < n; i++)
		{
			q.Add(s[i] * 100000L + i);
			while (q.Min % 100000 < t) q.Pop();

			t = (int)q.Pop() % 100000;
			r.Add(s[t]);
		}

		return string.Join("", r);
	}
}

class BstPQ1<T> : SortedSet<T>
{
	public T Pop() { var r = Min; Remove(r); return r; }
}
