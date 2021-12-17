using System;
using System.Collections.Generic;
using System.Linq;

class L2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();

		var st = new ST1<(int v, int l)>(n, (x, y) => x.v <= y.v ? x : y, (int.MaxValue, -1), a.Select((v, i) => (v, i)).ToArray());
		var r = new List<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var (t, x, y) = Read3();
			x--;

			if (t == 1)
			{
				st.Set(x, (y, x));
			}
			else
			{
				var (p, l) = st.Get(x, y);
				x = l;

				r.Clear();
				for (int tp = p; tp == p; (tp, x) = st.Get(x + 1, y))
				{
					r.Add(x + 1);
				}
				Console.WriteLine($"{r.Count} " + string.Join(" ", r));
			}
		}
		Console.Out.Flush();
	}
}
