using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main()
	{
		var r = new List<string>();
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], q = h[1];
		var ps = new int[h[0]].Select(_ => Console.ReadLine().Split()).Select(x => Tuple.Create(x[0], int.Parse(x[1])));
		var queue = new Queue<Tuple<string, int>>(ps);

		var t = 0;
		while (queue.Any())
		{
			var v = queue.Dequeue();
			if (v.Item2 <= q)
			{
				r.Add($"{v.Item1} {t += v.Item2}");
			}
			else
			{
				t += q;
				queue.Enqueue(Tuple.Create(v.Item1, v.Item2 - q));
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
