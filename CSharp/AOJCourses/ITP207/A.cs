using System;
using System.Collections.Generic;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new HashSet<string>();
		for (int i = 0; i < n; i++)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
			{
				set.Add(q[1]);
				Console.WriteLine(set.Count);
			}
			else
				Console.WriteLine(set.Contains(q[1]) ? 1 : 0);
		}
	}
}
