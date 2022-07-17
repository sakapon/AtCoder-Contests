using System;
using CoderLib8.Collections;

class Q061R
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var dq = new Deque<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (t, x) in qs)
		{
			if (t == 1)
			{
				dq.PushFirst(x);
			}
			else if (t == 2)
			{
				dq.PushLast(x);
			}
			else
			{
				Console.WriteLine(dq[x - 1]);
			}
		}
		Console.Out.Flush();
	}
}
