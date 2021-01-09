using System;
using System.Collections.Generic;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var d = new Dictionary<string, string>();
		for (int i = 0; i < n; i++)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
				d[q[1]] = q[2];
			else if (q[0] == "1")
				Console.WriteLine(d.ContainsKey(q[1]) ? d[q[1]] : "0");
			else
				d.Remove(q[1]);
		}
	}
}
