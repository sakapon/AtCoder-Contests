using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new List<int>[n + 1];
		for (int i = 1; i <= n; i++) a[i] = Console.ReadLine().Split().Select(int.Parse).ToList();

		var q = new List<int[]>();
		var found = new bool[n + 1];
		Action<int> find = i =>
		{
			if (found[i] || !a[i].Any()) return;
			var j = a[i][0];
			if (a[j][0] != i) return;
			found[j] = true;
			q.Add(new[] { i, j });
		};

		for (int i = 1; i <= n; i++) find(i);
		var c = 0;
		for (; q.Any(); c++)
		{
			var ps = q.ToArray();
			q.Clear();
			Array.Clear(found, 1, n);
			foreach (var p in ps) foreach (var i in p) a[i].RemoveAt(0);
			foreach (var p in ps) foreach (var i in p) find(i);
		}
		Console.WriteLine(a.Any(l => l?.Count > 0) ? -1 : c);
	}
}
