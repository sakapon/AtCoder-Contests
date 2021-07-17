using System;
using System.Linq;

class Q064
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();

		var d = Enumerable.Range(0, n - 1).Select(i => (long)a[i + 1] - a[i]).ToArray();
		var sum = d.Sum(Math.Abs);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var (l, r, v) = Read3();
			l -= 2;
			r -= 1;

			if (l >= 0)
			{
				sum -= Math.Abs(d[l]);
				d[l] += v;
				sum += Math.Abs(d[l]);
			}
			if (r < n - 1)
			{
				sum -= Math.Abs(d[r]);
				d[r] -= v;
				sum += Math.Abs(d[r]);
			}
			Console.WriteLine(sum);
		}
		Console.Out.Flush();
	}
}
