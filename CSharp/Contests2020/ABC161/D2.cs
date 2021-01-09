using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		if (k <= 9) { Console.WriteLine(k); return; }

		var t = 9;
		var q = new Queue<long>(Enumerable.Range(1, 9).Select(i => (long)i));

		while (q.TryDequeue(out var x))
			for (long d = x % 10 - 1; d <= x % 10 + 1; d++)
			{
				if (d < 0 || d > 9) continue;
				q.Enqueue(x * 10 + d);
				if (++t == k) { Console.WriteLine(x * 10 + d); return; }
			}
	}
}
