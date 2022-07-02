using System;
using CoderLib8.Collections;

class ES
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();

		var st = new RAQ(n + 1);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var q = Read();
			if (q[0] == 0)
				st.AddRange(q[1], q[2] + 1, q[3]);
			else
				Console.WriteLine(st[q[1]]);
		}
		Console.Out.Flush();
	}
}
