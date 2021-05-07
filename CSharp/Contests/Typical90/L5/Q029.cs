using System;
using CoderLib8.Trees;

class Q029
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (w, n) = Read2();
		var qs = Array.ConvertAll(new bool[n], _ => Read2());

		var st = new LST<int, int>(w,
			(x, y) => x == int.MinValue ? y : x, int.MinValue,
			Math.Max, int.MinValue,
			(x, p, _, l) => x == int.MinValue ? p : x,
			new int[w]);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (l, r) in qs)
		{
			var m = st.Get(l - 1, r) + 1;
			st.Set(l - 1, r, m);
			Console.WriteLine(m);
		}
		Console.Out.Flush();
	}
}
