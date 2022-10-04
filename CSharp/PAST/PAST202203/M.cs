using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WBTrees;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var p = Read();
		var sb = new StringBuilder();

		var comp = ComparerHelper<int>.Create(true);
		var map = new WBMap<int, int>(comp);
		map.Initialize(Enumerable.Range(0, n).Select(i => (p[i], i)));

		while (qc-- > 0)
		{
			var q = Read();
			if (q[0] == 1)
			{
				var i = q[1] - 1;
				var x = q[2];

				map.Remove(p[i]);
				map[x] = i;
				p[i] = x;
			}
			else if (q[0] == 2)
			{
				var i = q[1] - 1;
				sb.Append(map.GetIndex(p[i]) + 1).AppendLine();
			}
			else
			{
				var r = q[1] - 1;
				sb.Append(map.GetAt(r).Item.Value + 1).AppendLine();
			}
		}
		Console.Write(sb);
	}
}
