using System;
using CoderLib8.Collections;

class Q044R
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var dq = new Deque<int>();
		foreach (var x in a)
			dq.PushLast(x);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				(dq[x - 1], dq[y - 1]) = (dq[y - 1], dq[x - 1]);
			}
			else if (t == 2)
			{
				dq.PushFirst(dq.PopLast());
			}
			else
			{
				Console.WriteLine(dq[x - 1]);
			}
		}
		Console.Out.Flush();
	}
}
