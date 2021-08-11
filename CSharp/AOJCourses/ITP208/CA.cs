using System;
using System.Collections.Generic;
using CoderLib6.DataTrees;

class CA
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var c = StringComparer.Ordinal;
		var map = new AvlMap<string, string>(c.Compare);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
				map[q[1]] = q[2];
			else if (q[0] == "1")
				Console.WriteLine(map.ContainsKey(q[1]) ? map[q[1]] : "0");
			else if (q[0] == "2")
				map.Remove(q[1]);
			else
				foreach (var p in map.GetItems(x => c.Compare(x, q[1]) >= 0, x => c.Compare(x, q[2]) <= 0))
					Console.WriteLine($"{p.Key} {p.Value}");
		}
		Console.Out.Flush();
	}
}
