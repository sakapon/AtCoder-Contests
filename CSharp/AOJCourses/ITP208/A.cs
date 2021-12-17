using System;
using System.Collections.Generic;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var d = new Dictionary<string, string>(StringComparer.Ordinal);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
				d[q[1]] = q[2];
			else
				Console.WriteLine(d[q[1]]);
		}
		Console.Out.Flush();
	}
}
