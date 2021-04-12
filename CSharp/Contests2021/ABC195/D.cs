using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, m, qc) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		var x = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		foreach (var (l, r) in qs)
		{
			var sum = 0;

			var bs = x.Where((v, i) => i < l - 1 || r <= i).ToArray();
			Array.Sort(bs);

			var wvs = ps.OrderBy(t => -t.Item2).ThenBy(t => t.Item1);
			foreach (var (w, v) in wvs)
			{
				for (int j = 0; j < bs.Length; j++)
				{
					if (bs[j] < w) continue;

					sum += v;
					bs[j] = 0;
					break;
				}
			}
			Console.WriteLine(sum);
		}
	}
}
