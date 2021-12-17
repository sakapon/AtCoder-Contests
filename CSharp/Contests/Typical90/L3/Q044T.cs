using System;
using System.Collections.Generic;
using CoderLib6.DataTrees.Bsts;

class Q044T
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var l = new AvlList<int>();
		foreach (var x in a)
			l.Add(x);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				// Node を操作することで高速化できます。
				(l[x - 1], l[y - 1]) = (l[y - 1], l[x - 1]);
			}
			else if (t == 2)
			{
				l.Prepend(l.RemoveAt(n - 1));
			}
			else
			{
				Console.WriteLine(l[x - 1]);
			}
		}
		Console.Out.Flush();
	}
}
