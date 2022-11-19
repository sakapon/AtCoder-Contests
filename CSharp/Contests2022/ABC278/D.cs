using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var qc = int.Parse(Console.ReadLine());

		var sb = new StringBuilder();
		var d = Enumerable.Range(1, n).ToDictionary(i => i, i => a[i - 1]);
		var v0 = 0L;

		while (qc-- > 0)
		{
			var q = ReadL();
			if (q[0] == 1)
			{
				d.Clear();
				v0 = q[1];
			}
			else if (q[0] == 2)
			{
				var i = (int)q[1];
				var x = q[2];
				if (d.ContainsKey(i)) d[i] += x;
				else d[i] = x;
			}
			else
			{
				var i = (int)q[1];
				sb.Append(d.ContainsKey(i) ? v0 + d[i] : v0).AppendLine();
			}
		}
		Console.Write(sb);
	}
}
