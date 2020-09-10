using System;
using System.Collections.Generic;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var d = new Map<string, string>("0");
		for (int i = 0; i < n; i++)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
				d[q[1]] = q[2];
			else if (q[0] == "1")
				Console.WriteLine(d[q[1]]);
			else
				d.Remove(q[1]);
		}
	}
}
