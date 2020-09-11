﻿using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var qs = new int[n].Select(_ => Console.ReadLine().Split()).ToArray();

		var keys = qs.Select(q => q[1]).Concat(qs.Where(q => q[0] == "3").Select(q => q[2])).Distinct().ToArray();
		Array.Sort(keys);
		var keymap = Enumerable.Range(0, keys.Length).ToDictionary(i => keys[i], i => i);

		var r = new List<string>();
		var set = new SortedSet<int>();
		var d = new Map<int, string>("0");

		foreach (var q in qs)
		{
			var keyInt = keymap[q[1]];
			if (q[0] == "0")
			{
				set.Add(keyInt);
				d[keyInt] = q[2];
			}
			else if (q[0] == "1")
				r.Add(d[keyInt]);
			else if (q[0] == "2")
			{
				d.Remove(keyInt);
				set.Remove(keyInt);
			}
			else
				r.AddRange(set.GetViewBetween(keyInt, keymap[q[2]]).Select(x => $"{keys[x]} {d[x]}"));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
