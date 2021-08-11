using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees;

class CA
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<string>();
		var map = new AvlMap<string, string>();

		for (int i = 0; i < n; i++)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
				map[q[1]] = q[2];
			else if (q[0] == "1")
				r.Add(map.ContainsKey(q[1]) ? map[q[1]] : "0");
			else if (q[0] == "2")
				map.Remove(q[1]);
			else
				r.AddRange(map.GetItems(x => x.CompareTo(q[1]) >= 0, x => x.CompareTo(q[2]) <= 0).Select(p => $"{p.Key} {p.Value}"));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
