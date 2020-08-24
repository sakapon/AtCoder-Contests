using System;
using System.Collections.Generic;

class B
{
	static void Main()
	{
		int n = int.Parse(Console.ReadLine());

		var r = new List<int>();
		var a = new int[2 * n];
		int i0 = n, i1 = n;

		for (int k = n; k > 0; k--)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				if (q[1] == 0 && i0 < i1) a[--i0] = q[2];
				else a[i1++] = q[2];
			}
			else if (q[0] == 1)
			{
				r.Add(a[i0 + q[1]]);
			}
			else
			{
				if (q[1] == 0 && i0 + 1 < i1) i0++;
				else i1--;
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
