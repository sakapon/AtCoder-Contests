using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var sum = a.Sum();
		if (sum % 3 != 0) return false;
		sum /= 3;

		var l = new List<int>();

		for (int x = 0; x < 1 << n; ++x)
		{
			var s = 0;
			for (int i = 0; i < n; ++i)
			{
				if ((x & (1 << i)) != 0)
				{
					s += a[i];
				}
			}
			if (s == sum) l.Add(x);
		}

		for (int i = 0; i < l.Count; i++)
		{
			for (int j = i + 1; j < l.Count; j++)
			{
				if ((l[i] & l[j]) == 0) return true;
			}
		}
		return false;
	}
}
