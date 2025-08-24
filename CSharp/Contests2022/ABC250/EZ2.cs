using System;
using System.Collections.Generic;
using System.Linq;

class EZ2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var aset = new HashSet<int>();
		var bset = new HashSet<int>();
		var ahash = new int[n + 1];
		var bhash = new int[n + 1];

		for (int i = 0; i < n; i++)
		{
			ahash[i + 1] = ahash[i] ^ (aset.Add(a[i]) ? a[i] * a[i] * 987654323 : 0);
			bhash[i + 1] = bhash[i] ^ (bset.Add(b[i]) ? b[i] * b[i] * 987654323 : 0);
		}

		var r = qs.Select(q => ahash[q.x] == bhash[q.y] ? "Yes" : "No");
		return string.Join("\n", r);
	}
}
