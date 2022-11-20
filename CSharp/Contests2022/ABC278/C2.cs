using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();

		var sb = new StringBuilder();
		var set = new HashSet<(int, int)>();

		while (qc-- > 0)
		{
			var q = Read();
			var a = q[1];
			var b = q[2];

			if (q[0] == 1)
			{
				set.Add((a, b));
			}
			else if (q[0] == 2)
			{
				set.Remove((a, b));
			}
			else
			{
				sb.AppendLine(set.Contains((a, b)) && set.Contains((b, a)) ? "Yes" : "No");
			}
		}
		Console.Write(sb);
	}
}
