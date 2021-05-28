using System;

class Q044
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var m = 1;

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				var i = MInt(x - m, n);
				var j = MInt(y - m, n);
				(a[i], a[j]) = (a[j], a[i]);
			}
			else if (t == 2)
			{
				m++;
			}
			else
			{
				var i = MInt(x - m, n);
				Console.WriteLine(a[i]);
			}
		}
		Console.Out.Flush();
	}

	static long MInt(long x, long M) => (x %= M) < 0 ? x + M : x;
}
