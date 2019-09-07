using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).OrderByDescending(x => x));

		var q = new int[n + 1].Select(_ => new Queue<int>()).ToArray();
		q[n].Enqueue(int.MaxValue);
		for (int hp, i = n; i >= 0; i--)
			while (q[i].Any())
			{
				if ((hp = s.Dequeue()) >= q[i].Dequeue()) { Console.WriteLine("No"); return; }
				for (var j = i - 1; j >= 0; j--) q[j].Enqueue(hp);
			}
		Console.WriteLine("Yes");
	}
}
