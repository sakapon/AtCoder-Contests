using System;

class Q010
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var s1 = new CumSum(Array.ConvertAll(ps, _ => _.Item1 == 1 ? _.Item2 : 0));
		var s2 = new CumSum(Array.ConvertAll(ps, _ => _.Item1 == 2 ? _.Item2 : 0));

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (l, r) in qs)
		{
			Console.WriteLine($"{s1.Sum(l - 1, r)} {s2.Sum(l - 1, r)}");
		}
		Console.Out.Flush();
	}
}

class CumSum
{
	long[] s;
	public CumSum(int[] a)
	{
		s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
	}
	public long Sum(int l_in, int r_ex) => s[r_ex] - s[l_in];
}
