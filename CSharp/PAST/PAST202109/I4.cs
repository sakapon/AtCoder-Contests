using System;
using System.Collections.Generic;

class I4
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var c = 0;

		for (int i = 0; i < n; i++)
		{
			while ((a[i] & 1) == 0)
			{
				a[i] >>= 1;
				c++;
			}
		}

		Array.Sort(a);

		var a2 = (long[])a.Clone();
		var c2 = c;

		var max = a2[^1];
		var l = new List<long>();

		for (int i = 0; i < n; i++)
		{
			while (3 * a2[i] <= max)
			{
				l.Add(a2[i]);
				a2[i] *= 3;
				c2--;
			}
		}

		if (c2 >= 0)
		{
			Array.Sort(a2);
			var r = a2[c2 % n];
			c2 /= n;
			while (c2-- > 0) r *= 3;
			return r;
		}
		else
		{
			a = l.ToArray();
			Array.Sort(a);
			return a[c];
		}
	}
}
