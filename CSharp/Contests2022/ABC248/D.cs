using System;
using System.Collections.Generic;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var indss = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int i = 0; i < n; i++)
		{
			indss[a[i]].Add(i + 1);
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (l, r, x) in qs)
		{
			var inds = indss[x];

			var jl = inds.BinarySearch(l);
			if (jl < 0) jl = ~jl;

			var jr = inds.BinarySearch(r + 1);
			if (jr < 0) jr = ~jr;

			Console.WriteLine(jr - jl);
		}
		Console.Out.Flush();
	}
}
