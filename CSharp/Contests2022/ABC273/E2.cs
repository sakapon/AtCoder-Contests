using System;
using System.Collections.Generic;
using System.Collections.Immutable;

class E2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());

		var d = new Dictionary<int, ImmutableStack<int>>();
		var root = ImmutableStack<int>.Empty.Push(-1);
		var a = root;

		var r = new int[qc];

		for (int k = 0; k < qc; k++)
		{
			var q = Console.ReadLine().Split();
			var c = q[0][0];

			if (c == 'A')
			{
				var x = int.Parse(q[1]);
				a = a.Push(x);
			}
			else if (c == 'D')
			{
				if (a.Peek() != -1) a = a.Pop();
			}
			else if (c == 'S')
			{
				var y = int.Parse(q[1]);
				d[y] = a;
			}
			else
			{
				var z = int.Parse(q[1]);
				if (d.ContainsKey(z))
				{
					a = d[z];
				}
				else
				{
					a = root;
				}
			}

			r[k] = a.Peek();
		}

		return string.Join(" ", r);
	}
}
