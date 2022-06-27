using System;
using System.Collections.Generic;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new HashSet<string>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 0; i < n; i++)
		{
			var q = Console.ReadLine().Split();
			if (q[0][0] == '0')
			{
				set.Add(q[1]);
				Console.WriteLine(set.Count);
			}
			else if (q[0][0] == '1')
				Console.WriteLine(set.Contains(q[1]) ? 1 : 0);
			else
				set.Remove(q[1]);
		}
		Console.Out.Flush();
	}
}
