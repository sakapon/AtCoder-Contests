using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var (x, y) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		const int max = 1 << 30;
		var r = max;
		var d = new Map<(int s, int t), int>(max);
		d[(0, 0)] = 0;

		foreach (var (a, b) in ps)
		{
			var da = d.ToArray();

			foreach (var item in da)
			{
				var (st, count) = item;
				var (s, t) = st;

				count++;
				s += a;
				t += b;
				s = Math.Min(s, 300);
				t = Math.Min(t, 300);

				if (s >= x && t >= y)
				{
					r = Math.Min(r, count);
				}
				else
				{
					d[(s, t)] = Math.Min(d[(s, t)], count);
				}
			}
		}

		if (r == max) return -1;
		return r;
	}
}

class Map<TK, TV> : Dictionary<TK, TV>
{
	TV _v0;
	public Map(TV v0 = default(TV)) { _v0 = v0; }

	public new TV this[TK key]
	{
		get { return ContainsKey(key) ? base[key] : _v0; }
		set { base[key] = value; }
	}
}
