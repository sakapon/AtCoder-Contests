﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees.SBTs;

class ABT
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var st = new MergeSBT<int>(n, Monoid.Int32_Min);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
			if (q[0] == 0)
				st[q[1]] = q[2];
			else
				Console.WriteLine(st[q[1], q[2] + 1]);
		Console.Out.Flush();
	}
}
