using System;

class J2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var bit = new BIT(n);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var k = q[1];

			if (q[0] == 1)
			{
				var v = bit.Sum(k <= n ? n - k + 1 : k - n, n + 1);
				Console.WriteLine(v % 2 == 0 ? k : 2 * n - k + 1);
			}
			else
			{
				bit.Add(k, 1);
			}
		}
		Console.Out.Flush();
	}
}
