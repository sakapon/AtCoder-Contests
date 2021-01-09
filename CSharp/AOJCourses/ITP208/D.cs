using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<string>();
		var set = new SortedSet<string>();
		var d = new MultiMap<string, string>();

		for (int i = 0; i < n; i++)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
			{
				set.Add(q[1]);
				d.Add(q[1], q[2]);
			}
			else if (q[0] == "1")
			{
				r.AddRange(d.ReadValues(q[1]));
			}
			else if (q[0] == "2")
			{
				d.Remove(q[1]);
				set.Remove(q[1]);
			}
			else
				r.AddRange(set.GetViewBetween(q[1], q[2]).SelectMany(x => d[x].Select(v => $"{x} {v}")));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

class MultiMap<TK, TV> : Dictionary<TK, List<TV>>
{
	static List<TV> empty = new List<TV>();

	public void Add(TK key, TV v)
	{
		if (ContainsKey(key)) this[key].Add(v);
		else this[key] = new List<TV> { v };
	}

	public List<TV> ReadValues(TK key) => ContainsKey(key) ? this[key] : empty;
}
