using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var s = Console.ReadLine();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var fi = 0;

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var x = q[1];
			if (q[0] == 1)
			{
				fi = (fi - x + n) % n;
			}
			else
			{
				Console.WriteLine(s[(fi + x - 1) % n]);
			}
		}
		Console.Out.Flush();
	}
}
