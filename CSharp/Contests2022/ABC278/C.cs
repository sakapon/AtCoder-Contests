using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();

		var sb = new StringBuilder();
		var d = new Dictionary<int, HashSet<int>>();

		while (qc-- > 0)
		{
			var q = Read();
			var a = q[1];
			var b = q[2];

			if (q[0] == 1)
			{
				if (!d.ContainsKey(a)) d[a] = new HashSet<int>();
				d[a].Add(b);
			}
			else if (q[0] == 2)
			{
				if (!d.ContainsKey(a)) d[a] = new HashSet<int>();
				d[a].Remove(b);
			}
			else
			{
				if (!d.ContainsKey(a)) d[a] = new HashSet<int>();
				if (!d.ContainsKey(b)) d[b] = new HashSet<int>();
				sb.AppendLine(d[a].Contains(b) && d[b].Contains(a) ? "Yes" : "No");
			}
		}
		Console.Write(sb);
	}
}
