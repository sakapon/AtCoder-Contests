using System;
using System.Collections.Generic;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var prev = new int[n + 1];
		var next = new int[n + 1];
		Array.Fill(prev, -1);
		Array.Fill(next, -1);

		var r = new List<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				next[q[1]] = q[2];
				prev[q[2]] = q[1];
			}
			else if (q[0] == 2)
			{
				next[q[1]] = -1;
				prev[q[2]] = -1;
			}
			else
			{
				r.Clear();
				var x = q[1];

				while (prev[x] != -1)
				{
					x = prev[x];
				}
				for (; x != -1; x = next[x])
				{
					r.Add(x);
				}

				Console.WriteLine($"{r.Count} " + string.Join(" ", r));
			}
		}
		Console.Out.Flush();
	}
}
