using System;
using System.Collections.Generic;
using System.Linq;

class C3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var qs = new int[n].Select(_ => Console.ReadLine().Split());

		var l = new List<int>();
		var fi = 0;

		var actions = new Dictionary<string, Action<string[]>>();
		actions["insert"] = q => l.Add(int.Parse(q[1]));
		actions["delete"] = q =>
		{
			var x = int.Parse(q[1]);
			for (int i = l.Count - 1; i >= fi; i--)
				if (l[i] == x) { l.RemoveAt(i); break; }
		};
		actions["deleteFirst"] = q => l.RemoveAt(l.Count - 1);
		actions["deleteLast"] = q => fi++;

		foreach (var q in qs) actions[q[0]](q);
		l.Reverse();
		Console.WriteLine(string.Join(" ", l.Take(l.Count - fi)));
	}
}
