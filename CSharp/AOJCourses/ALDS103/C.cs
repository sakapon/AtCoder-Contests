using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var qs = new int[n].Select(_ => Console.ReadLine().Split());

		var l = new LinkedList<int>();

		var actions = new Dictionary<string, Action<string[]>>();
		actions["insert"] = q => l.AddFirst(int.Parse(q[1]));
		actions["delete"] = q => l.Remove(int.Parse(q[1]));
		actions["deleteFirst"] = q => l.RemoveFirst();
		actions["deleteLast"] = q => l.RemoveLast();

		foreach (var q in qs)
			actions[q[0]](q);
		Console.WriteLine(string.Join(" ", l));
	}
}
