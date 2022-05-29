using System;
using System.Collections.Generic;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var set = new SortedSet<int>();
		var d = new Dictionary<int, int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				var x = q[1];
				if (d.ContainsKey(x))
				{
					d[x]++;
				}
				else
				{
					set.Add(x);
					d[x] = 1;
				}
			}
			else if (q[0] == 2)
			{
				var x = q[1];
				var c = q[2];
				if (d.ContainsKey(x))
				{
					if (d[x] <= c)
					{
						set.Remove(x);
						d.Remove(x);
					}
					else
					{
						d[x] -= c;
					}
				}
			}
			else
			{
				Console.WriteLine(set.Max - set.Min);
			}
		}
		Console.Out.Flush();
	}
}
