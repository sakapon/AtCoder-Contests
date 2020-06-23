using System;
using System.Collections.Generic;
using System.Linq;

class L
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var L10 = 1000000L;
		var h = Read();
		int n = h[0], k = h[1], d = h[2];
		var a = Read().Select((x, i) => L10 * x + i).ToArray();
		if (n < d * (k - 1) + 1) { Console.WriteLine(-1); return; }

		var r = new List<long>();
		var pq = PQ<long>.Create(a.Take(n - d * (k - 1)).ToArray());
		var q = new Queue<long>(a.Skip(n - d * (k - 1)));

		var t = pq.Pop();
		var ci = t % L10;
		r.Add(t / L10);

		for (int i = 1; i < k; i++)
		{
			for (int j = 0; j < d; j++)
				pq.Push(q.Dequeue());

			while ((t = pq.Pop()) % L10 < ci + d) ;
			ci = t % L10;
			r.Add(t / L10);
		}
		Console.WriteLine(string.Join(" ", r));
	}
}
