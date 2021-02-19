using System;
using System.Collections.Generic;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var (h, w, qc) = Read3();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var r = (long)h * w;
		var map = new Map<int, int>(h + 1);

		foreach (var (y, x) in qs)
		{
			var t = map[x];
			if (t > y)
			{
				r -= t - y;
				map[x] = y;
			}
			Console.WriteLine(r);
		}
		Console.Out.Flush();
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
