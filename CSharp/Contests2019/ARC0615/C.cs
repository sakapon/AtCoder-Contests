using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).OrderBy(x => x).ToArray();

		var M = 0;
		var l = new List<string>();
		if (a[0] >= 0)
		{
			M = a[0];
			for (int i = 1; i < n - 1; i++)
			{
				l.Add($"{M} {a[i]}");
				M -= a[i];
			}
			l.Add($"{a[n - 1]} {M}");
			M = a[n - 1] - M;
		}
		else if (a[n - 1] <= 0)
		{
			M = a[n - 1];
			for (int i = 0; i < n - 1; i++)
			{
				l.Add($"{M} {a[i]}");
				M -= a[i];
			}
		}
		else
		{
			M = a[0];
			int i = n - 2;
			for (; a[i] > 0; i--)
			{
				l.Add($"{M} {a[i]}");
				M -= a[i];
			}
			l.Add($"{a[n - 1]} {M}");
			M = a[n - 1] - M;
			for (; i > 0; i--)
			{
				l.Add($"{M} {a[i]}");
				M -= a[i];
			}
		}
		Console.WriteLine(M);
		foreach (var item in l) Console.WriteLine(item);
	}
}
