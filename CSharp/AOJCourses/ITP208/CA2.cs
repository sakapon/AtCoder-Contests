using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees;

class CA2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());

		var keys = qs.Select(q => q[1]).Concat(qs.Where(q => q[0] == "3").Select(q => q[2])).Distinct().ToArray();
		Array.Sort(keys);
		var keymap = Enumerable.Range(0, keys.Length).ToDictionary(i => keys[i], i => i);

		var map = new AvlMap<int, string>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var key = keymap[q[1]];
			if (q[0] == "0")
				map[key] = q[2];
			else if (q[0] == "1")
				Console.WriteLine(map.ContainsKey(key) ? map[key] : "0");
			else if (q[0] == "2")
				map.Remove(key);
			else
				foreach (var p in map.GetItems(x => x >= key, x => x <= keymap[q[2]]))
					Console.WriteLine($"{keys[p.Key]} {p.Value}");
		}
		Console.Out.Flush();
	}
}
