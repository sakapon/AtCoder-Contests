using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());

		var keys = qs.Select(q => q[1]).Concat(qs.Where(q => q[0] == "3").Select(q => q[2])).Distinct().ToArray();
		Array.Sort(keys, StringComparer.Ordinal);
		var keymap = Enumerable.Range(0, keys.Length).ToDictionary(i => keys[i], i => i);

		var set = new SortedSet<int>();
		var d = new Dictionary<int, string>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var key = keymap[q[1]];
			if (q[0] == "0")
			{
				set.Add(key);
				d[key] = q[2];
			}
			else if (q[0] == "1")
			{
				Console.WriteLine(d.ContainsKey(key) ? d[key] : "0");
			}
			else if (q[0] == "2")
			{
				d.Remove(key);
				set.Remove(key);
			}
			else
			{
				foreach (var x in set.GetViewBetween(key, keymap[q[2]]))
					Console.WriteLine($"{keys[x]} {d[x]}");
			}
		}
		Console.Out.Flush();
	}
}
