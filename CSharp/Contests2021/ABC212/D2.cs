using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());

		var sd = new SortedDictionary<long, int>();
		var d = 0L;

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var q = Read();
			if (q[0] == 1)
			{
				var v = q[1] - d;
				sd[v] = sd.GetValueOrDefault(v) + 1;
			}
			else if (q[0] == 2)
			{
				d += q[1];
			}
			else
			{
				var (v, c) = sd.First();
				if (c > 1) sd[v] = c - 1;
				else sd.Remove(v);
				Console.WriteLine(v + d);
			}
		}
		Console.Out.Flush();
	}
}
