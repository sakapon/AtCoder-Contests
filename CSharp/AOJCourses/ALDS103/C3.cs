using System;
using System.Collections.Generic;
using System.Linq;

class C3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var l = new List<int>();
		var fi = 0;

		var ac = new Dictionary<string, Action<string[]>>();
		ac["insert"] = q => l.Add(int.Parse(q[1]));
		ac["delete"] = q =>
		{
			var i = l.LastIndexOf(int.Parse(q[1]));
			if (i != -1) l.RemoveAt(i);
		};
		ac["deleteFirst"] = q => l.RemoveAt(l.Count - 1);
		ac["deleteLast"] = q => fi++;

		while (n-- > 0)
		{
			var q = Console.ReadLine().Split();
			ac[q[0]](q);
		}
		l.Reverse();
		Console.WriteLine(string.Join(" ", l.Take(l.Count - fi)));
	}
}
