using System;
using System.Collections.Generic;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new SortedSet<string>(StringComparer.Ordinal);
		var d = new Dictionary<string, string>(StringComparer.Ordinal);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
			{
				set.Add(q[1]);
				d[q[1]] = q[2];
			}
			else if (q[0] == "1")
			{
				Console.WriteLine(d.ContainsKey(q[1]) ? d[q[1]] : "0");
			}
			else if (q[0] == "2")
			{
				d.Remove(q[1]);
				set.Remove(q[1]);
			}
			else
			{
				foreach (var x in set.GetViewBetween(q[1], q[2]))
					Console.WriteLine($"{x} {d[x]}");
			}
		}
		Console.Out.Flush();
	}
}
