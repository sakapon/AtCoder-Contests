using System;
using System.Collections.Generic;

class B
{
	static void Main()
	{
		var r = new List<int>();
		var a = new int[800001];
		var i0 = 400001;
		var i1 = 399999;

		for (int k = int.Parse(Console.ReadLine()); k > 0; k--)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				if (i0 > i1)
				{
					a[--i0] = q[2];
					i1++;
				}
				else
				{
					if (q[1] == 0) a[--i0] = q[2];
					else a[++i1] = q[2];
				}
			}
			else if (q[0] == 1)
			{
				r.Add(a[i0 + q[1]]);
			}
			else
			{
				if (i0 == i1)
				{
					i0++;
					i1--;
				}
				else
				{
					if (q[1] == 0) i0++;
					else i1--;
				}
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
