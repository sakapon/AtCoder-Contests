using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<string>();
		var set = new SortedSet<string>();
		var d = new Map<string, string>("0");

		for (int i = 0; i < n; i++)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
			{
				set.Add(q[1]);
				d[q[1]] = q[2];
			}
			else if (q[0] == "1")
				r.Add(d[q[1]]);
			else if (q[0] == "2")
			{
				d.Remove(q[1]);
				set.Remove(q[1]);
			}
			else
				r.AddRange(set.GetViewBetween(q[1], q[2]).Select(x => $"{x} {d[x]}"));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

class Map<TK, TV> : Dictionary<TK, TV>
{
	TV _v0;
	public Map(TV v0 = default(TV)) { _v0 = v0; }

	public new TV this[TK key]
	{
		get { return ContainsKey(key) ? base[key] : _v0; }
		set { base[key] = value; }
	}
}
