using System;
using System.Collections.Generic;
using System.Linq;

class M0
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());
		var sum = d.Values.Sum(c => c * (c - 1) / 2);

		Dictionary<int, int> Set(int l, int r, int x)
		{
			var d = new Dictionary<int, int> { [x] = r - l };

			for (int i = l; i < r; i++)
			{
				var x0 = a[i];
				d[x0] = d.GetValueOrDefault(x0) - 1;
				a[i] = x;
			}
			return d;
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (l, r, x) in qs)
		{
			var delta = Set(l - 1, r, x);

			foreach (var (k, dc) in delta)
			{
				if (dc == 0) continue;

				var c = d.GetValueOrDefault(k);
				sum -= c * (c - 1) / 2;
				c += dc;
				sum += c * (c - 1) / 2;

				d[k] = c;
			}

			Console.WriteLine(sum);
		}
		Console.Out.Flush();
	}
}
