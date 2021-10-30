using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var pref = new int[n + 1];
		var next = new int[n + 1];
		Array.Fill(pref, -1);
		Array.Fill(next, -1);

		var r = new List<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				next[q[1]] = q[2];
				pref[q[2]] = q[1];
			}
			else if (q[0] == 2)
			{
				next[q[1]] = -1;
				pref[q[2]] = -1;
			}
			else
			{
				r.Clear();

				var x = q[1];

				for (int y = x; y != -1; y = pref[y])
				{
					r.Add(y);
				}
				r.Reverse();
				r.RemoveAt(r.Count - 1);
				for (int y = x; y != -1; y = next[y])
				{
					r.Add(y);
				}

				Console.WriteLine($"{r.Count} " + string.Join(" ", r));
			}
		}
		Console.Out.Flush();
	}
}
