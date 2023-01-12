using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var sb = new StringBuilder();

		foreach (var q in qs)
		{
			var k = q[1] - 1;
			if (q[0] == 1)
			{
				a[k] = q[2];
			}
			else
			{
				sb.Append(a[k]).AppendLine();
			}
		}
		Console.Write(sb);
	}
}
